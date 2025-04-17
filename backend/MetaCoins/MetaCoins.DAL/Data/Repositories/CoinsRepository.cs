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
                .ToListAsync();
        }

        public async Task<Coin> GetCoinByIdAsync(Guid coinId)
        {
            return await _context.Coins
                .Include(c => c.OwnershipRecords)
                    .ThenInclude(o => o.Wallet)
                        .ThenInclude(w => w.User)
                .Include(c => c.Wallet)
                .Include(c => c.Creator)
                .Include(c => c.Likes)
                .FirstOrDefaultAsync(c => c.Id == coinId);
        }

        public async Task CreateCoinOwnerRecordAsync(CoinOwnerRecord ownerRecord)
        {
            await _context.CoinOwnerRecords.AddAsync(ownerRecord);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}