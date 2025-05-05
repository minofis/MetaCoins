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

        public async Task<Wallet> GetWalletByIdAsync(Guid walletId)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Coins)
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

        public async Task<Wallet> GetWalletByUserIdAsync(Guid userId)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Coins)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Wallet?> GetWalletByUsernameAsync(string username)
        {
            return await _context.Wallets
                .Include(w => w.User)
                .Include(w => w.Coins)
                .FirstOrDefaultAsync(w => w.User.UserName == username);
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }
    }
}