using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Enums.Coin;
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
        public async Task CreateCoinAsync(Guid userId, string prompt)
        {
            var user = await _usersService.GetUserByIdAsync(userId);
            var wallet = await _walletsService.GetWalletByUsernameAsync(user.UserName);

            var coin = new Coin
            {
                Id = Guid.NewGuid(),
                Prompt = prompt,
                CoinStatusId = 1,
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

            await _coinsRepo.UpdateCoinAsync(coin);
        }

        public async Task<PaginatedResult<Coin>> GetCoinsByQueryAsync(CoinQueryObject query)
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

            if (!string.IsNullOrEmpty(query.Title))
            {
                queryCoins = queryCoins.Where(c => c.Title.Contains(query.Title));
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

        public async Task<List<CoinOwnerRecord>> GetOwnerRecordsByCoinIdAsync(Guid coinId)
        {
            return await _coinsRepo.GetOwnerRecordsByCoinIdAsync(coinId) ?? new List<CoinOwnerRecord>();
        }

        public async Task UpdateCoinDetailsAsync(Guid userId, Guid coinId, string? title, string? description)
        {
            var coin = await EnsureCoinOwnedByUserAsync(coinId, userId);

            if(coin.CoinStatusId == (int)CoinStatuses.Deleted)
            {
                throw new ArgumentException($"Coin with ID {coinId} is deleted");
            }
            
            coin.Title = title ?? coin.Title;
            coin.Description = description ?? coin.Description;
            await _coinsRepo.UpdateCoinAsync(coin);
        }

        public async Task UpdateCoinStatusAsync(Guid userId, Guid coinId, string status)
        {
            if (!Enum.TryParse<CoinStatuses>(status, true, out var newStatus))
            {
                throw new ArgumentException($"Invalid coin status: {status}");
            }

            var coin = await EnsureCoinOwnedByUserAsync(coinId, userId);

            if (coin.CoinStatusId == (int)newStatus)
            {
                throw new ArgumentException($"Coin with ID {coinId} is already with status {newStatus}");
            } 
            else if(coin.CoinStatusId == (int)CoinStatuses.Deleted)
            {
                throw new ArgumentException($"Coin with ID {coinId} is deleted");
            }

            await _coinsRepo.UpdateCoinStatusAsync(coin.Id, (int)newStatus);
        }

        public async Task<Coin> EnsureCoinOwnedByUserAsync(Guid coinId, Guid userId)
        {
            var coin = await GetCoinByIdAsync(coinId);

            if (coin.Wallet.UserId != userId)
                throw new ArgumentException($"User with ID {userId} does not own the coin with ID {coinId}");
            return coin;
        }

        public async Task CreateCoinOwnerRecordAsync(Guid coinId, Guid walletId)
        {
            var ownerRecord = new CoinOwnerRecord{
                Id = Guid.NewGuid(),
                WalletId = walletId,
                CoinId = coinId,
                AcquiredAt = DateTime.UtcNow
            };

            await _coinsRepo.CreateCoinOwnerRecordAsync(ownerRecord);
        }
    }
}