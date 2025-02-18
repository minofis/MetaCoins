using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user, IList<string> roles);
    }
}