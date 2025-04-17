using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface ILikesRepository
    {
        Task<List<Coin>> GetLikedCoinsByUsernameAsync(string username);
        Task LikeCoinAsync(Like like);
        Task UnlikeCoinAsync(Guid userId, Guid coinId);
        Task<bool> IsCoinLikedAsync(Guid userId, Guid coinId);
    }
}