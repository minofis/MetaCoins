using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Enums.CoinSellOrder;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinSellOrdersService : ICoinSellOrdersService
    {
        private readonly ICoinSellOrdersRepository _coinSellOrdersRepo;
        private readonly IWalletsService _walletsService;
        private readonly ICoinsService _coinsService;
        public CoinSellOrdersService(ICoinSellOrdersRepository coinSellOrdersRepo, IWalletsService walletsService, ICoinsService coinsService)
        {
            _coinSellOrdersRepo = coinSellOrdersRepo;
            _coinsService = coinsService;
            _walletsService = walletsService;
        }
        public async Task CreateCoinSellOrderAsync(Guid coinId, Guid sellerUserId, decimal price)
        {
            await _coinsService.EnsureCoinOwnedByUserAsync(coinId, sellerUserId);

            var sellerWallet = await _walletsService.GetWalletByUserIdAsync(sellerUserId);

            var coinSellOrder = new CoinSellOrder
            {
                Id = Guid.NewGuid(),
                StatusId = (int)CoinSellOrderStatuses.Active,
                SellerWalletId = sellerWallet.Id,
                CoinId = coinId,
                Price = price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _coinSellOrdersRepo.CreateCoinSellOrderAsync(coinSellOrder);
        }

        public async Task<CoinSellOrder> GetCoinSellOrderById(Guid coinSellOrderId)
        {
            var coinSellOrder = await _coinSellOrdersRepo.GetCoinSellOrderById(coinSellOrderId)
                ?? throw new ArgumentException($"Coin sell order with ID {coinSellOrderId} not found.");

            return coinSellOrder;
        }

        public async Task UpdateCoinSellOrderStatusAsync(Guid coinSellOrderId, Guid userId, string status)
        {
            if (!Enum.TryParse<CoinSellOrderStatuses>(status, true, out var newStatus))
            {
                throw new ArgumentException($"Invalid coin sell order status: {status}");
            }

            var coinSellOrder = await _coinSellOrdersRepo.GetCoinSellOrderById(coinSellOrderId);

            await _coinsService.EnsureCoinOwnedByUserAsync(coinSellOrder.CoinId, userId);

            if (coinSellOrder.StatusId == (int)newStatus)
            {
                throw new ArgumentException($"Coin sell order with ID {coinSellOrderId} is already with status {newStatus}");
            } 
            else if(coinSellOrder.StatusId == (int)CoinSellOrderStatuses.Cancelled)
            {
                throw new ArgumentException($"Coin sell order with ID {coinSellOrderId} is cancelled");
            }

            await _coinSellOrdersRepo.UpdateCoinSellOrderStatusAsync(coinSellOrderId, (int)newStatus);
        }
    }
}