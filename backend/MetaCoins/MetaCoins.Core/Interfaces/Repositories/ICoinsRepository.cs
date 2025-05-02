using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinsRepository
    {
        Task<List<Coin>> GetAllCoinsAsync();
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task<List<CoinOwnerRecord>> GetOwnerRecordsByCoinIdAsync(Guid coinId);
        Task UpdateCoinStatusAsync(Guid coinId, int statusId);
        Task CreateCoinOwnerRecordAsync(CoinOwnerRecord ownerRecord);
        Task<List<Coin>> GetCoinsByUsernameAsync(string username);
        Task CreateCoinAsync(Coin coin);
        Task UpdateCoinAsync(Coin coin);
    }
}