using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Enums.Coin;
using MetaCoins.Core.Entities.Helpers;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinsService
    {
        Task<PaginatedResult<Coin>> GetCoinsByQueryAsync(CoinQueryObject query);
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task<List<CoinOwnerRecord>> GetOwnershipRecordsByCoinIdAsync(Guid coinId);
        Task CreateCoinAsync(Guid userId, string prompt);
        Task UpdateCoinDetailsAsync(Guid userId, Guid coinId, string? title, string? description);
        Task UpdateCoinStatusAsync(Guid userId, Guid coinId, string status);
    }
}