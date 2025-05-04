using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinTransactionsRepository
    {
        Task<CoinTransaction> GetCoinTransactionById(Guid coinTransactionId);
        Task CreateCoinTransactionAsync(CoinTransaction coinTransaction);
        Task UpdateCoinTransactionStatusAsync(Guid coinTransactionId, int statusId);
        Task UpdateCoinTransactionAsync(CoinTransaction coinTransaction);
    }
}