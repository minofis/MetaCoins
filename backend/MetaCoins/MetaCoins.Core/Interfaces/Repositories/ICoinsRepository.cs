using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinsRepository
    {
        Task<List<Coin>> GetAllCoinsAsync();
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task CreateCoinAsync(Coin coin);
        Task CreateCoinOwnerRecordAsync(CoinOwnerRecord ownerRecord);
        Task SaveChangesAsync();
    }
}