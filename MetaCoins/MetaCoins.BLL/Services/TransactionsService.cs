using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepo;
        private readonly IWalletsRepository _walletsRepo;
        private readonly ICoinsRepository _coinsRepo;
        public TransactionsService(ITransactionsRepository transactionsRepo, IWalletsRepository walletsRepo, ICoinsRepository coinsRepo)
        {
            _transactionsRepo = transactionsRepo;
            _walletsRepo = walletsRepo;
            _coinsRepo = coinsRepo;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionsRepo.GetAllTransactionsAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
        {
            // Get transaction by specificated ID
            var transaction = await _transactionsRepo.GetTransactionByIdAsync(transactionId)
                ?? throw new ArgumentException($"Transaction with ID {transactionId} not found.");

            return transaction;
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            await _transactionsRepo.CreateTransactionAsync(transaction);
        }

        public async Task TransferCoinAsync(string senderUsername, string recipientUsername, Guid coinId)
        {
            // Get recipient and sender wallets
            var senderWallet = await _walletsRepo.GetWalletByUsernameAsync(senderUsername)
                ?? throw new ArgumentException($"User with username '{senderUsername}' not found.");
            
            var recipientWallet = await _walletsRepo.GetWalletByUsernameAsync(recipientUsername)
                ?? throw new ArgumentException($"User with username '{recipientUsername}' not found.");

            // Check for the coin of the sender wallet
            var coin = senderWallet.Coins.FirstOrDefault(c => c.Id == coinId)
                ?? throw new ArgumentException($"You don't have this coin in your wallet.");

            // Change owner of the coin
            coin.WalletId = recipientWallet.Id;

            // Create a transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                CoinId = coin.Id,
                TypeId = 1,
                StatusId = 1,
                SenderWalletId = senderWallet.Id,
                RecipientWalletId = recipientWallet.Id,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };
            await _transactionsRepo.CreateTransactionAsync(transaction);

            // Add transaction to both wallets
            senderWallet.SentTransactions.Add(transaction);
            recipientWallet.RecivedTransactions.Add(transaction);

            var ownerRecord = new CoinOwnerRecord{
                Id = Guid.NewGuid(),
                WalletId = recipientWallet.Id,
                CoinId = coin.Id,
                AcquiredAt = DateTime.Now.ToUniversalTime()
            };
            await _coinsRepo.CreateCoinOwnerRecordAsync(ownerRecord);

            coin.OwnershipRecords.Add(ownerRecord);

            // Save changes
            await _walletsRepo.SaveChangesAsync();
        }
    }
}