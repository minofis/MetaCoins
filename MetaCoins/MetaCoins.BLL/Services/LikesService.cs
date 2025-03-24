using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _likesRepo;
        private readonly IUsersService _usersService;
        public LikesService(ILikesRepository likesRepo, IUsersService usersService)
        {
            _likesRepo = likesRepo;
            _usersService = usersService;
        }

        public async Task<List<Coin>> GetLikedCoinsByUsernameAsync(string username)
        {
            // Get likes by specificated username
            var likedCoins = await _likesRepo.GetLikedCoinsByUsernameAsync(username)
                ?? throw new ArgumentException($"Likes of user with username {username} not found.");

            return likedCoins;
        }

        public async Task LikeCoinAsync(Guid userId, Guid coinId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if(user.Likes.Any(l => l.CoinId == coinId))
            {
                throw new ArgumentException($"Coin with ID {coinId} already liked.");
            }

            var like = new Like
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CoinId = coinId,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            await _likesRepo.LikeCoinAsync(like);
        }

        public async Task UnlikeCoinAsync(Guid userId, Guid coinId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if(!user.Likes.Any(l => l.CoinId == coinId))
            {
                throw new ArgumentException($"Coin with ID {coinId} not liked.");
            }

            await _likesRepo.UnlikeCoinAsync(userId, coinId);
        }

        public async Task<bool> IsCoinLikedAsync(Guid userId, Guid coinId)
        {
            var isLiked = await _likesRepo.IsCoinLikedAsync(userId, coinId);
            
            return isLiked;
        }
    }
}