using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IProfilesRepository
    {
        Task<Profile> GetProfileByUsernameAsync(string username);
    }
}