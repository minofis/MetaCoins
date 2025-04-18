using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class CoinsService : ICoinsService
    {
        private readonly ICoinsRepository _coinsRepo;
        private readonly IImageService _imageService;
        private readonly IWalletsService _walletsService;
        private readonly IUsersService _usersService;
        public CoinsService(ICoinsRepository coinsRepo, IImageService imageService, IWalletsService walletsService, IUsersService usersService)
        {
            _coinsRepo = coinsRepo;
            _imageService = imageService;
            _walletsService = walletsService;
            _usersService = usersService;
        }
        public async Task CreateCoinAsync(Guid userId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);
            var wallet = await _walletsService.GetWalletByUsernameAsync(user.UserName);

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

        public async Task<PaginatedResult<Coin>> GetAllCoinsAsync(CoinQueryObject query)
        {
            var coins = await _coinsRepo.GetAllCoinsAsync();

            if (coins == null || coins.Count == 0)
            {
                return new PaginatedResult<Coin>();
            }

            var queryCoins = coins.AsQueryable();

            if (!string.IsNullOrEmpty(query.Username))
            {
                queryCoins = queryCoins.Where(c => c.Wallet.User.UserName.Contains(query.Username));
            }

            IOrderedQueryable<Coin>? orderedQuery = null;
            foreach(var sort in query.SortBy)
            {
                if (orderedQuery == null)
                {
                    orderedQuery = (sort.Field.ToLower(), sort.Descending) switch
                    {
                        ("likes", true) => queryCoins.OrderByDescending(c => c.LikesCount),
                        ("likes", false) => queryCoins.OrderBy(c => c.LikesCount),
                        ("createdat", true) => queryCoins.OrderByDescending(c => c.CreatedAt),
                        ("createdat", false) => queryCoins.OrderBy(c => c.CreatedAt),
                        _ => orderedQuery
                    };
                }
                else
                {
                    orderedQuery = (sort.Field.ToLower(), sort.Descending) switch
                    {
                        ("likes", true) => orderedQuery.ThenByDescending(c => c.LikesCount),
                        ("likes", false) => orderedQuery.ThenBy(c => c.LikesCount),
                        ("createdat", true) => orderedQuery.ThenByDescending(c => c.CreatedAt),
                        ("createdat", false) => orderedQuery.ThenBy(c => c.CreatedAt),
                        _ => orderedQuery
                    };
                }
            }

            queryCoins = orderedQuery ?? queryCoins;

            var paginatedResult = new PaginatedResult<Coin>
            {
                Items = queryCoins.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToList(),
                TotalItems = coins.Count(),
                Page = query.PageNumber,
                PageSize = query.PageSize
            };
            
            return paginatedResult;
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