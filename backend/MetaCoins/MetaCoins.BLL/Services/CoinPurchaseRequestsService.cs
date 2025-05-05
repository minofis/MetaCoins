using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Enums;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinPurchaseRequestsService : ICoinPurchaseRequestsService
    {
        private readonly ICoinPurchaseRequestsRepository _coinPurchaseRequestsRepo;
        private readonly IWalletsService _walletsService;
        private readonly ICoinSellOrdersService _coinSellOrdersService;
        public CoinPurchaseRequestsService(ICoinPurchaseRequestsRepository coinPurchaseRequestsRepo, ICoinSellOrdersService coinSellOrdersService, IWalletsService walletsService)
        {
            _coinPurchaseRequestsRepo = coinPurchaseRequestsRepo;
            _walletsService = walletsService;
            _coinSellOrdersService = coinSellOrdersService;
        }

        public async Task<CoinPurchaseRequest> GetCoinPurchaseRequestByIdAsync(Guid coinPurchaseRequestId)
        {
            var coinPurchaseRequest = await _coinPurchaseRequestsRepo.GetCoinPurchaseRequestByIdAsync(coinPurchaseRequestId)
                ?? throw new ArgumentException($"Coin purchase request with ID {coinPurchaseRequestId} not found.");

            return coinPurchaseRequest;
        }

        public async Task CreateCoinPurchaseRequestAsync(Guid coinSellOrderId, Guid buyerUserId)
        {
            var coinSellOrder = await _coinSellOrdersService.GetCoinSellOrderById(coinSellOrderId);

            var buyerWallet = await _walletsService.GetWalletByUserIdAsync(buyerUserId);

            if(!buyerWallet.HasEnoughBalance(coinSellOrder.Price))
                throw new InvalidOperationException($"Wallet with ID {buyerWallet.Id} does not have enough balance.");

            var coinPurchaseRequest = new CoinPurchaseRequest
            {
                Id = Guid.NewGuid(),
                StatusId = (int)CoinPurchaseRequestStatuses.Pending,
                BuyerWalletId = buyerWallet.Id,
                CoinSellOrderId = coinSellOrderId,
                CreatedAt = DateTime.UtcNow,
            };

            await _coinPurchaseRequestsRepo.CreateCoinPurchaseRequestAsync(coinPurchaseRequest);
        }

        public async Task UpdateCoinPurchaseRequestStatusAsync(Guid userId, Guid coinPurchaseRequestId, string status)
        {
            if (!Enum.TryParse<CoinPurchaseRequestStatuses>(status, true, out var newStatus))
            {
                throw new ArgumentException($"Invalid coin purchase request status: {status}");
            }

            int newStatusId = (int)newStatus;

            var coinPurchaseRequest = await EnsureCoinPurchaseRequestOwnedByUserAsync(coinPurchaseRequestId, userId);

            if (coinPurchaseRequest.StatusId == newStatusId)
            {
                throw new ArgumentException($"Coin purchase request with ID {coinPurchaseRequestId} is already with status {newStatus}");
            } 
            
            if(coinPurchaseRequest.StatusId == (int)CoinPurchaseRequestStatuses.Approved)
            {
                throw new ArgumentException($"Coin purchase request with ID {coinPurchaseRequestId} is Approved");
            }
            
            if(coinPurchaseRequest.StatusId == (int)CoinPurchaseRequestStatuses.Cancelled)
            {
                throw new ArgumentException($"Coin purchase request with ID {coinPurchaseRequestId} is Cancelled");
            }

            await _coinPurchaseRequestsRepo.UpdateCoinPurchaseRequestStatusAsync(coinPurchaseRequest.Id, newStatusId);
        }

        public async Task<CoinPurchaseRequest> EnsureCoinPurchaseRequestOwnedByUserAsync(Guid coinPurchaseRequestId, Guid userId)
        {
            var coinPurchaseRequest = await GetCoinPurchaseRequestByIdAsync(coinPurchaseRequestId);

            if (coinPurchaseRequest.BuyerWallet.UserId != userId)
                throw new ArgumentException($"User with ID {userId} does not own the coin purchase request with ID {coinPurchaseRequestId}");
                
            return coinPurchaseRequest;
        }
    }
}