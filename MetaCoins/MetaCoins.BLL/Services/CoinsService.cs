using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

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

        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            var coins = await _coinsRepo.GetAllCoinsAsync();

            return coins;
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

        public async Task<List<Coin>> GetCoinsSortedAsync(string sortBy, bool descending)
        {
            var coins = await _coinsRepo.GetAllCoinsAsync();
            
            if (coins == null || !coins.Any())
            {
                return new List<Coin>();
            }

            IEnumerable<Coin> sortedCoins = coins;

            switch (sortBy.ToLower())
            {
                case "likes": 
                    sortedCoins = descending
                        ? coins.OrderByDescending(c => c.LikesCount)
                        : coins.OrderBy(c => c.LikesCount);
                    break;

                case "createdat": 
                    sortedCoins = descending
                        ? coins.OrderByDescending(c => c.CreatedAt)
                        : coins.OrderBy(c => c.CreatedAt);
                    break;
                    
                default: 
                    break;
            }

            return sortedCoins.ToList();
        }
    }
}