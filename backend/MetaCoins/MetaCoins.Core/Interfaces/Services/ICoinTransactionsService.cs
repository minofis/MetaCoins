using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinTransactionsService
    {
        Task<CoinTransaction> GetCoinTransactionById(Guid coinTransactionId);
        Task TransferCoinAsync(Guid senderUserId, string recipientUsername, Guid coinId);
        Task PurchaseCoinFromSellOrderAsync(Guid coinSellOrderId, Guid buyerUserId);
    }
}