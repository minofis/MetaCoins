using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Enums.CoinTransaction;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinTransactionsService : ICoinTransactionsService
    {
        private readonly ICoinTransactionsRepository _coinTransactionsRepo;
        private readonly IWalletsService _walletsService;
        private readonly ICoinsService _coinsService;
        public CoinTransactionsService(ICoinTransactionsRepository coinTransactionsRepo, IWalletsService walletsService, ICoinsService coinsService)
        {
            _coinTransactionsRepo = coinTransactionsRepo;
            _coinsService = coinsService;
            _walletsService = walletsService;
        }

        public async Task<CoinTransaction> GetCoinTransactionById(Guid coinTransactionId)
        {
            var coinTransaction = await _coinTransactionsRepo.GetCoinTransactionById(coinTransactionId)
                ?? throw new ArgumentException($"Coin transaction with ID {coinTransactionId} not found.");

            return coinTransaction;
        }

        public async Task TransferCoinAsync(Guid senderUserId, string recipientUsername, Guid coinId)
        {
            var coin = await _coinsService.EnsureCoinOwnedByUserAsync(coinId, senderUserId);

            var senderWallet = await _walletsService.GetWalletByUserIdAsync(senderUserId);
            
            var recipientWallet = await _walletsService.GetWalletByUsernameAsync(recipientUsername);

            var coinTransaction = new CoinTransaction
            {
                Id = Guid.NewGuid(),
                CoinId = coinId,
                TypeId = (int)CoinTransactionTypes.Transfer,
                StatusId = (int)CoinTransactionStatuses.Pending,
                SenderWalletId = senderWallet.Id,
                RecipientWalletId = recipientWallet.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            coin.WalletId = recipientWallet.Id;

            await _coinTransactionsRepo.CreateCoinTransactionAsync(coinTransaction);

            senderWallet.SentTransactions.Add(coinTransaction);
            recipientWallet.RecivedTransactions.Add(coinTransaction);

            await _coinsService.CreateCoinOwnerRecordAsync(coinId, recipientWallet.Id);
        }

        public Task PurchaseCoinFromSellOrderAsync(Guid coinSellOrderId, Guid buyerUserId)
        {
            throw new NotImplementedException();
        }
    }
}