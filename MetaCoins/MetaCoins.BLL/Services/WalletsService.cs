using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IWalletsRepository _walletsRepo;
        private readonly IUsersService _usersService;
        public WalletsService(IWalletsRepository walletsRepo, IUsersService usersService)
        {
            _walletsRepo = walletsRepo;
            _usersService = usersService;
        }

        public async Task<List<Wallet>> GetAllWalletsAsync()
        {
            return await _walletsRepo.GetAllWalletsAsync();
        }

        public async Task<List<Wallet>> GetWalletsByFilterAsync(string? ownerUsername, string? status, string? type)
        {
            var filterDto = new WalletsFilterDto{
                OwnerUsername = ownerUsername,
                Status = status,
                Type = type,
            };

            return await _walletsRepo.GetWalletsByFilterAsync(filterDto);
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

        public async Task CreateWalletAsync(Guid userId, string type)
        {
            // Get wallets by user id
            var wallets = await _usersService.GetWalletsByIdAsync(userId);

            // Check if wallet with this type already exist
            if (wallets != null && wallets.Any(a => a.Type.Name == type && a.Status.Name != "Closed"))
            {
                throw new ArgumentException($"Wallet with type {type} already exists");
            }

            int typeId;
            try
            {
                // Get wallet type id by name
                typeId = await _walletsRepo.GetWalletTypeId(type);
            }
            catch
            {
                throw new ArgumentException($"Wallet type with name {type} not found.");
            }

            // Create a wallet
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                TypeId = typeId,
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
    }
}