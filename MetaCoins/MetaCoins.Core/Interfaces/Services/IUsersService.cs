using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IUsersService
    {
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetUserByIdAsync(Guid userId);
        Task<UserEntity> GetUserByUsernameAsync(string username);
        Task<List<Like>> GetUserLikesByUsernameAsync(string username);
        Task<Wallet> GetUserWalletByIdAsync(Guid userId);
        Task<Guid> GetCurrentUserIdAsync();
        Task Register(string username, string email, string password);
        Task<string> Login(string username, string password);
        Task AssignRoleToUserAsync(Guid userId, string roleName);
        Task RemoveRoleFromUserAsync(Guid userId, string roleName);
    }
}