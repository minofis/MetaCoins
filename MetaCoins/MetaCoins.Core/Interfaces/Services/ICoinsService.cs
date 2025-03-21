using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinsService
    {
        Task<List<Coin>> GetAllCoinsAsync(QueryObject query);
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task<List<CoinOwnerRecord>> GetCoinOwnershipRecordsByIdAsync(Guid coinId);
        Task CreateCoinAsync(string ownerUsername);
    }
}