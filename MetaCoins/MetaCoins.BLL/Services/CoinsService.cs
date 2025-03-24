using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.BLL.Services
{
    public class CoinsService : ICoinsService
    {
        private readonly ICoinsRepository _coinsRepo;
        private readonly IImageService _imageService;
        private readonly IWalletsService _walletsService;
        public CoinsService(ICoinsRepository coinsRepo, IImageService imageService, IWalletsService walletsService)
        {
            _coinsRepo = coinsRepo;
            _imageService = imageService;
            _walletsService = walletsService;
        }
        public async Task CreateCoinAsync(string ownerUsername)
        {
            var wallet = await _walletsService.GetWalletByUsernameAsync(ownerUsername);

            var coin = new Coin
            {
                Id = Guid.NewGuid(),
                ImageUrl = string.Empty,
                WalletId = wallet.Id,
                CreatorId = wallet.Id,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
            await _coinsRepo.CreateCoinAsync(coin);

            var ownerRecord = new CoinOwnerRecord{
                Id = Guid.NewGuid(),
                WalletId = wallet.Id,
                CoinId = coin.Id,
                AcquiredAt = DateTime.Now.ToUniversalTime()
            };
            await _coinsRepo.CreateCoinOwnerRecordAsync(ownerRecord);

            coin.OwnershipRecords.Add(ownerRecord);
            coin.ImageUrl = await _imageService.GenerateImage();

            await _coinsRepo.SaveChangesAsync();
        }

        public async Task<List<Coin>> GetAllCoinsAsync(QueryObject query)
        {
            var coins = await _coinsRepo.GetAllCoinsAsync();

            if (coins == null || coins.Count == 0)
            {
                return new List<Coin>();
            }

            var queryCoins = coins.AsQueryable();

            if (!string.IsNullOrEmpty(query.Username))
            {
                queryCoins = queryCoins.Where(c => c.Wallet.User.UserName.Contains(query.Username));
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "likes": 
                        queryCoins = query.Descending
                            ? queryCoins.OrderByDescending(c => c.LikesCount)
                            : queryCoins.OrderBy(c => c.LikesCount);
                        break;

                    case "createdat": 
                        queryCoins = query.Descending
                            ? queryCoins.OrderByDescending(c => c.CreatedAt)
                            : queryCoins.OrderBy(c => c.CreatedAt);
                        break;
                        
                    default: 
                        break;
                }
            }
            
            return queryCoins.ToList();
        }

        public async Task<Coin> GetCoinByIdAsync(Guid coinId)
        {
            var coin = await _coinsRepo.GetCoinByIdAsync(coinId)
                ?? throw new ArgumentException($"Coin with ID {coinId} not found.");;

            return coin;
        }

        public async Task<List<CoinOwnerRecord>> GetCoinOwnershipRecordsByIdAsync(Guid coinId)
        {
            var coin = await _coinsRepo.GetCoinByIdAsync(coinId)
                ?? throw new ArgumentException($"Coin with ID {coinId} not found.");;

            var ownerRecords = coin.OwnershipRecords.OrderBy(o => o.AcquiredAt).ToList()
                ?? throw new ArgumentException($"There are no any ownership records.");

            return ownerRecords;
        }
    }
}