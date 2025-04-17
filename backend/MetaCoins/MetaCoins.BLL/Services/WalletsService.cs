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

        public async Task<List<Wallet>> GetAllWalletsAsync()
        {
            return await _walletsRepo.GetAllWalletsAsync();
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid walletId)
        {
            // Get wallet by specificated ID
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            return wallet;
        }

        public async Task CreateWalletAsync(Guid walletId, Guid userId)
        {
            // Create a wallet
            var wallet = new Wallet
            {
                Id = walletId,
                UserId = userId,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            // Add the wallet
            await _walletsRepo.CreateWalletAsync(wallet);
        }

        public async Task<List<Transaction>> GetRecivedTransactionsByIdAsync(Guid walletId)
        {
            // Get wallet by specificated ID
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            // Get recived transactions of the wallet
            var recivedTransactions = wallet.RecivedTransactions
                ?? throw new ArgumentException($"Recived transactions of wallet with ID {walletId} not found.");

            return recivedTransactions;
        }

        public async Task<List<Transaction>> GetSentTransactionsByIdAsync(Guid walletId)
        {
            // Get wallet by specificated ID
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            // Get sent transactions of the wallet
            var sentTransactions = wallet.SentTransactions
                ?? throw new ArgumentException($"Sent transactions of wallet with ID {walletId} not found.");

            return sentTransactions;
        }

        public async Task<List<Coin>> GetWalletCoinsByIdAsync(Guid walletId)
        {
            // Get wallet by specificated ID
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            // Get coins of the wallet
            var coins = wallet.Coins.ToList()
                ?? throw new ArgumentException($"Coins of wallet with ID {walletId} not found.");

            return coins;
        }

        public async Task<Wallet> GetWalletByUsernameAsync(string username)
        {
            // Get wallet by specificated username
            var wallet = await _walletsRepo.GetWalletByUsernameAsync(username)
                ?? throw new ArgumentException($"User with username {username} not found.");

            return wallet;
        }

        public async Task<List<Coin>> GetWalletCoinsByUsernameAsync(string username)
        {
            // Get wallet by specificated username
            var wallet = await _walletsRepo.GetWalletByUsernameAsync(username)
                ?? throw new ArgumentException($"Wallet with username {username} not found.");

            // Get coins of the wallet
            var coins = wallet.Coins.ToList()
                ?? throw new ArgumentException($"Coins of wallet with username {username} not found.");

            return coins;
        }
    }
}