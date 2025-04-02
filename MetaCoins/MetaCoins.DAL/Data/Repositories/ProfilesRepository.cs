using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.DAL.Data.Repositories
{
    public class ProfilesRepository : IProfilesRepository
    {
        private readonly MetaCoinsDbContext _context;
        public ProfilesRepository(MetaCoinsDbContext context)
        {
            _context = context;
        }
        public async Task<Profile> GetProfileByUsernameAsync(string username)
        {
            return await _context.Profiles
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.User.UserName == username);
        }
    }
}