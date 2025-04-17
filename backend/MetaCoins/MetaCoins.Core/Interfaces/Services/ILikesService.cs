using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface ILikesService
    {
        Task<List<Coin>> GetLikedCoinsByUsernameAsync(string username);
        Task LikeCoinAsync(Guid userId, Guid coinId);
        Task UnlikeCoinAsync(Guid userId, Guid coinId);
        Task<bool> IsCoinLikedAsync(Guid userId, Guid coinId);
    }
}