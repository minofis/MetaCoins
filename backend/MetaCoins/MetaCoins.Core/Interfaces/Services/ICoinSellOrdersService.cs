using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ICoinSellOrdersService
    {
        Task<CoinSellOrder> GetCoinSellOrderById(Guid coinSellOrderId);
        Task CreateCoinSellOrderAsync(Guid coinId, Guid sellerUserId, decimal price);
        Task UpdateCoinSellOrderStatusAsync(Guid coinSellOrderId, Guid userId, string status);
    }
}