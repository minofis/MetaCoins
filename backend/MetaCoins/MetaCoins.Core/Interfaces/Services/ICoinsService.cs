using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinsService
    {
        Task<PaginatedResult<Coin>> GetAllCoinsAsync(CoinQueryObject query);
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task<List<CoinOwnerRecord>> GetCoinOwnershipRecordsByIdAsync(Guid coinId);
        Task CreateCoinAsync(Guid userId);
    }
}