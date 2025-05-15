using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinsService
    {
        Task<PaginatedResult<Coin>> GetCoinsByQueryAsync(CoinQueryObject query);
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task CreateCoinOwnerRecordAsync(Guid coinId, Guid walletId);
        Task<List<CoinOwnerRecord>> GetOwnerRecordsByCoinIdAsync(Guid coinId);
        Task CreateCoinAsync(Guid userId, string prompt);
        Task UpdateCoinDetailsAsync(Guid userId, Guid coinId, string? title, string? description);
        Task UpdateCoinStatusAsync(Guid userId, Guid coinId, string status);
        Task<Coin> EnsureCoinOwnedByUserAsync(Guid coinId, Guid userId);
    }
}