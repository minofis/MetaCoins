using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class WalletsRepository : IWalletsRepository
    {
        private readonly MetaCoinsDbContext _context;
        public WalletsRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Wallet>> GetAllWalletsAsync()
        {
            return await _context.Wallets
                .Include(w => w.SentTransactions)
                .Include(w => w.RecivedTransactions)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid walletId)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Coins)
                    .ThenInclude(c => c.OwnershipRecords)
                .Include(w => w.SentTransactions)
                .Include(w => w.RecivedTransactions)
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

        public async Task<Wallet?> GetWalletByUsernameAsync(string username)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Coins)
                    .ThenInclude(c => c.OwnershipRecords)
                .Include(w => w.Coins)
                    .ThenInclude(c => c.Likes)
                .Include(w => w.Coins)
                    .ThenInclude(c => c.Creator)
                        .ThenInclude(c => c.User)
                .Include(w => w.SentTransactions)
                .Include(w => w.RecivedTransactions)
                .FirstOrDefaultAsync(w => w.User.UserName == username);
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}