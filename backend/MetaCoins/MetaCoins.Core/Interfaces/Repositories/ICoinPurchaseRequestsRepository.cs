using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinPurchaseRequestsRepository
    {
        Task<CoinPurchaseRequest> GetCoinPurchaseRequestByIdAsync(Guid coinPurchaseRequestId);
        Task CreateCoinPurchaseRequestAsync(CoinPurchaseRequest coinPurchaseRequest);
        Task UpdateCoinPurchaseRequestStatusAsync(Guid coinPurchaseRequestId, int statusId);
    }
}