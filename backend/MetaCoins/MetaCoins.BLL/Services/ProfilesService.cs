using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class ProfilesService : IProfilesService
    {
        private readonly IProfilesRepository _profilesRepo;
        public ProfilesService(IProfilesRepository profilesRepo)
        {
            _profilesRepo = profilesRepo;
        }
        public async Task<Profile> GetProfileByUsernameAsync(string username)
        {
            // Get profile by specificated username
            var profile = await _profilesRepo.GetProfileByUsernameAsync(username)
                ?? throw new ArgumentException($"User with username {username} not found.");

            return profile;
        }
    }
}