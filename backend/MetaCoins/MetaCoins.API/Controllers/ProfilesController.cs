using MetaCoins.API.Dtos.ProfileDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class ProfilesController : ControllerBase
    {   
        private readonly IProfilesService _profilesService;
        private readonly IUsersService _usersService;
        public ProfilesController(IProfilesService profilesService, IUsersService usersService)
        {
            _profilesService = profilesService;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<ProfileResponseDto>> GetProfileByUsername(string username)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var profile = await _profilesService.GetProfileByUsernameAsync(username);

                var profileResponseDto = new ProfileResponseDto
                {
                    Id = profile.Id,
                    Username = profile.User.UserName,
                    Description = profile.Description
                };

                return Ok(profileResponseDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new {message = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }
    }
}