using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ICoinSellOrdersRepository
    {
        Task<CoinSellOrder> GetCoinSellOrderById(Guid coinSellOrderId);
        Task CreateCoinSellOrderAsync(CoinSellOrder coinSellOrder);
        Task UpdateCoinSellOrderStatusAsync(Guid coinSellOrderId, int statusId);
        Task UpdateCoinSellOrderAsync(CoinSellOrder coinSellOrder);
    }
}