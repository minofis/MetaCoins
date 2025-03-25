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

        public async Task<WalletDetails> GetWalletDetailsByIdAsync(Guid walletId)
        {
            // Get wallet by specificated ID
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            // Get details of the wallet
            var walletDetails = wallet.Details
                ?? throw new ArgumentException($"Details of wallet with ID {walletId} not found.");

            return walletDetails;
        }

        public async Task CreateWalletAsync(Guid walletId, Guid userId)
        {
            // Create a wallet
            var wallet = new Wallet
            {
                Id = walletId,
                StatusId = 1,
                UserId = userId
            };

            wallet.Details = new WalletDetails
            {
                WalletId = wallet.Id,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            // Add the wallet
            await _walletsRepo.CreateWalletAsync(wallet);
        }

        public async Task UpdateWalletStatusByIdAsync(Guid walletId, string status)
        {
            var wallet = await _walletsRepo.GetWalletByIdAsync(walletId)
                ?? throw new ArgumentException($"Wallet with ID {walletId} not found.");

            if (wallet.Status.Name == "Closed")
            {
                throw new ArgumentException("Wallet is closed");
            }
            if (wallet.Status.Name == status)
            {
                throw new ArgumentException($"Wallet is already {status}");
            }

            // Get wallet status id by name
            int statusId = await _walletsRepo.GetWalletStatusId(status);

            if (statusId == 0)
            {
                throw new ArgumentException($"Wallet status with name {status} not found.");
            }

            await _walletsRepo.UpdateWalletStatusByIdAsync(walletId, statusId);
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