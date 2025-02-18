using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinsService
    {
        Task<List<Coin>> GetAllCoinsAsync();
        Task<Coin> GetCoinByIdAsync(Guid coinId);
        Task<List<CoinOwnerRecord>> GetCoinOwnershipRecordsByIdAsync(Guid coinId);
        Task CreateCoinAsync(Guid walletId);
    }
}