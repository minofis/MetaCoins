using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinPurchaseRequestsRepository
    {
        Task<CoinPurchaseRequest> GetCoinPurchaseRequestById(Guid coinPurchaseRequestId);
        Task CreateCoinPurchaseRequestAsync(CoinPurchaseRequest coinPurchaseRequest);
        Task UpdateCoinPurchaseRequestStatusAsync(Guid coinPurchaseRequestId, int statusId);
        Task DeleteCoinPurchaseRequestAsync(Guid coinPurchaseRequestId);
    }
}