using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinsService
    {
        Task<List<Coin>> GetAllCoinsAsync();
        Task<List<Coin>> GetCoinsSortedAsync(string sortBy, bool descending);
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task<List<CoinOwnerRecord>> GetCoinOwnershipRecordsByIdAsync(Guid coinId);
        Task CreateCoinAsync(string ownerUsername);
    }
}