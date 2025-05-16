using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Enums;
using MetaCoins.Core.Entities.Enums.CoinSellOrder;
using MetaCoins.Core.Entities.Enums.CoinTransaction;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinTransactionsService : ICoinTransactionsService
    {
        private readonly ICoinTransactionsRepository _coinTransactionsRepo;
        private readonly ICoinPurchaseRequestsService _coinPurchaseRequestsService;
        private readonly ICoinSellOrdersService _coinSellOrdersService;
        private readonly IWalletsService _walletsService;
        private readonly IEmailService _emailService;
        private readonly ICoinsService _coinsService;
        public CoinTransactionsService(
            ICoinTransactionsRepository coinTransactionsRepo, 
            ICoinPurchaseRequestsService coinPurchaseRequestsService, 
            IWalletsService walletsService, 
            ICoinsService coinsService, 
            ICoinSellOrdersService coinSellOrdersService,
            IEmailService emailService)
        {
            _coinTransactionsRepo = coinTransactionsRepo;
            _coinsService = coinsService;
            _walletsService = walletsService;
            _coinPurchaseRequestsService = coinPurchaseRequestsService;
            _coinSellOrdersService = coinSellOrdersService;
            _emailService = emailService;
        }

        public async Task<List<CoinTransaction>> GetSentCoinTransactionsAsync(Guid userId)
        {
            return await _coinTransactionsRepo.GetSentCoinTransactionsAsync(userId) ?? new List<CoinTransaction>();
        }

        public async Task<List<CoinTransaction>> GetRecivedCoinTransactionsAsync(Guid userId)
        {
            return await _coinTransactionsRepo.GetRecivedCoinTransactionsAsync(userId) ?? new List<CoinTransaction>();
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
                StatusId = (int)CoinTransactionStatuses.Completed,
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

        public async Task ApproveCoinPurchaseRequestAsync(Guid coinPurchaseRequestId, Guid sellerUserId)
        {
            var coinPurchaseRequest = await _coinPurchaseRequestsService.GetCoinPurchaseRequestByIdAsync(coinPurchaseRequestId);
            
            await _coinPurchaseRequestsService.EnsureCoinPurchaseRequestOwnedByUserAsync(coinPurchaseRequestId, coinPurchaseRequest.BuyerWallet.UserId);

            var coin = await _coinsService.EnsureCoinOwnedByUserAsync(coinPurchaseRequest.CoinSellOrder.CoinId, sellerUserId);

            var sellerWallet = await _walletsService.GetWalletByUserIdAsync(sellerUserId);

            var buyerWallet = await _walletsService.GetWalletByIdAsync(coinPurchaseRequest.BuyerWalletId);

            var coinTransaction = new CoinTransaction
            {
                Id = Guid.NewGuid(),
                CoinId = coin.Id,
                TypeId = (int)CoinTransactionTypes.Sale,
                StatusId = (int)CoinTransactionStatuses.Completed,
                SenderWalletId = sellerWallet.Id,
                RecipientWalletId = buyerWallet.Id,
                CoinPurchaseRequestId = coinPurchaseRequest.Id,
                CoinSellOrderId = coinPurchaseRequest.CoinSellOrderId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            coin.WalletId = buyerWallet.Id;

            buyerWallet.DecreaseBalance(coinPurchaseRequest.CoinSellOrder.Price);
            sellerWallet.IncreaseBalance(coinPurchaseRequest.CoinSellOrder.Price);

            await _coinTransactionsRepo.CreateCoinTransactionAsync(coinTransaction);

            sellerWallet.SentTransactions.Add(coinTransaction);
            buyerWallet.RecivedTransactions.Add(coinTransaction);

            await _coinsService.CreateCoinOwnerRecordAsync(coin.Id, buyerWallet.Id);

            await _coinPurchaseRequestsService.UpdateCoinPurchaseRequestStatusAsync(
                buyerWallet.UserId, 
                coinPurchaseRequestId, 
                CoinPurchaseRequestStatuses.Approved.ToString());

            await _coinSellOrdersService.UpdateCoinSellOrderStatusAsync(
                coinPurchaseRequest.CoinSellOrderId,
                buyerWallet.UserId,
                CoinSellOrderStatuses.Completed.ToString());
            
            // Sending an email about a accepted purchase request to the buyer
            var buyerEmail = buyerWallet.User.Email;
            var messageToBuyer = $"Dear {buyerWallet.User.UserName}. User with username '{sellerWallet.User.UserName}' has approved your purchase request with ID {coinPurchaseRequest.Id}.";
            var subjectToBuyer = "Your purchase request is approved!";

            await _emailService.SendEmailAsync(buyerEmail, subjectToBuyer, messageToBuyer);

            // Sending an email about a successfull sale to the seller
            var sellerEmail = sellerWallet.User.Email;
            var messageToSeller = $"Dear {sellerWallet.User.UserName}. Your coin with ID {coin.Id} was successfully sold to the user with username '{buyerWallet.User.UserName}'.";
            var subjectToSeller = "Your order to sale the coin has been successfully completed!";

            await _emailService.SendEmailAsync(sellerEmail, subjectToSeller, messageToSeller);
        }
    }
}