using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class CoinsRepository : ICoinsRepository
    {
        private readonly MetaCoinsDbContext _context;
        public CoinsRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        public async Task CreateCoinAsync(Coin coin)
        {
            await _context.Coins.AddAsync(coin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            return await _context.Coins
                .Include(c => c.Wallet)
                    .ThenInclude(w => w.User)
                .Include(c => c.Creator)
                    .ThenInclude(w => w.User)
                .Include(c => c.Likes)
                .Include(c => c.CoinStatus)
                .ToListAsync();
        }

        public async Task<Coin> GetCoinByIdAsync(Guid coinId)
        {
            return await _context.Coins
                .Include(c => c.Wallet)
                    .ThenInclude(w => w.User)
                .Include(c => c.Creator)
                    .ThenInclude(c => c.User)
                .Include(c => c.Likes)
                .Include(c => c.CoinStatus)
                .FirstOrDefaultAsync(c => c.Id == coinId);
        }

        public async Task CreateCoinOwnerRecordAsync(CoinOwnerRecord ownerRecord)
        {
            await _context.CoinOwnerRecords.AddAsync(ownerRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoinAsync(Coin coin)
        {
            _context.Coins.Update(coin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Coin>> GetCoinsByUsernameAsync(string username)
        {
            return await _context.Coins
                .Include(c => c.Wallet)
                    .ThenInclude(c => c.User)
                .Where(c => c.Wallet.User.UserName == username)
                .ToListAsync();
        }

        public async Task<List<CoinOwnerRecord>> GetOwnerRecordsByCoinIdAsync(Guid coinId)
        {
            return await _context.CoinOwnerRecords
                .Include(cor => cor.Wallet)
                    .ThenInclude(w => w.User)
                .Where(cor => cor.CoinId == coinId)
                .ToListAsync();
        }

        public async Task UpdateCoinStatusAsync(Guid coinId, int statusId)
        {
            await _context.Coins
                .Where(c => c.Id == coinId)
                .ExecuteUpdateAsync(s => s.SetProperty(c => c.CoinStatusId, statusId));
        }
    }
}