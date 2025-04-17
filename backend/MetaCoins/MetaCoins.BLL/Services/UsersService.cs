using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Identity;
using MetaCoins.Core.Interfaces.Auth;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MetaCoins.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersService(
            IJwtProvider jwtProvider, 
            UserManager<UserEntity> userManager, 
            RoleManager<RoleEntity> roleManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _jwtProvider = jwtProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Include(u => u.Wallet)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            return await _userManager.Users
                .Include(u => u.Likes)
                .Include(u => u.Votes)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new ArgumentException($"User with ID {userId} not found.");
        }

        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return await _userManager.Users
                .Include(u => u.Likes)
                .FirstOrDefaultAsync(u => u.UserName == username)
                ?? throw new ArgumentException($"User with username {username} not found.");
        }

        public async Task<string> Login(string username, string password)
        {
            // Get user by username
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username)
                ?? throw new ArgumentException($"User with username {username} not found.");

            // Verify that password is correct
            var result = await _userManager.CheckPasswordAsync(user, password);

            // Validate password
            if (result == false)
            {
                throw new ArgumentException("Login is failed");
            }

            var roles = await _userManager.GetRolesAsync(user);

            // Generate jwt token for the user
            var token = _jwtProvider.GenerateToken(user, roles);

            // Return token
            return token;
        }

        public async Task Register(string username, string email, string password)
        {
            if(await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username) != null)
            {
                throw new ArgumentException($"User with username {username} alredy exist");
            }

            if(await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email) != null)
            {
                throw new ArgumentException($"User with email {email} alredy exist");
            }

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = username,
                Email = email,
                WalletId = Guid.NewGuid()
            };

            // Create a new wallet
            user.Wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            // Create a new profile
            user.Profile = new Profile
            {
                Id = Guid.NewGuid(),
                Description = "I like MetaCoins :3",
                UserId = user.Id
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new ArgumentException($"Error: {error.Description}");
                }
            }

            await _userManager.AddToRoleAsync(user, "Customer");
        }

        public async Task AssignRoleToUserAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with ID {userId} not found.");

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                throw new ArgumentException($"Role with name {roleName} not found.");
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task RemoveRoleFromUserAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with ID {userId} not found.");

            await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<Guid> GetCurrentUserIdAsync()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value);
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _userManager.Users.AnyAsync(u => u.Id == userId);
        }
    }
}