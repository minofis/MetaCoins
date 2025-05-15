using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IWalletsRepository _walletsRepo;
        public WalletsService(IWalletsRepository walletsRepo)
        {
            _walletsRepo = walletsRepo;
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid walletId)
        {
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            return wallet;
        }

        public async Task CreateWalletAsync(Guid walletId, Guid userId)
        {
            var wallet = new Wallet
            {
                Id = walletId,
                UserId = userId,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            await _walletsRepo.CreateWalletAsync(wallet);
        }

        public async Task<Wallet> GetWalletByUsernameAsync(string username)
        {
            var wallet = await _walletsRepo.GetWalletByUsernameAsync(username)
                ?? throw new ArgumentException($"Wallet with username {username} not found.");

            return wallet;
        }

        public async Task<Wallet> GetWalletByUserIdAsync(Guid userId)
        {
            var wallet = await _walletsRepo.GetWalletByUserIdAsync(userId)
                ?? throw new ArgumentException($"Wallet with userId {userId} not found.");

            return wallet;
        }
    }
}