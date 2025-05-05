using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinPurchaseRequestsService
    {
        Task<CoinPurchaseRequest> GetCoinPurchaseRequestByIdAsync(Guid coinPurchaseRequestId);
        Task CreateCoinPurchaseRequestAsync(Guid coinSellOrderId, Guid buyerUserId);
        Task UpdateCoinPurchaseRequestStatusAsync(Guid userId, Guid coinPurchaseRequestId, string status);
        Task<CoinPurchaseRequest> EnsureCoinPurchaseRequestOwnedByUserAsync(Guid coinPurchaseRequestId, Guid userId);
    }
}