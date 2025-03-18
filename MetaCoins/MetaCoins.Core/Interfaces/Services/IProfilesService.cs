using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IProfilesService
    {
        Task<Profile> GetProfileByUsernameAsync(string username);
    }
}