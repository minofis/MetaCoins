using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        public ProfilesController(IProfilesService profilesService, IUsersService usersService, IMapper mapper)
        {
            _profilesService = profilesService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<ProfileResponseDto>> GetProfileByUsername(string username)
        {
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == null)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Get profile by the specified username
                var profile = await _profilesService.GetProfileByUsernameAsync(username);

                // Map the profile entity to response DTO
                var profileResponseDto = _mapper.Map<ProfileResponseDto>(profile);

                // Return a 200 Ok response with the profile
                return Ok(profileResponseDto);
            }
            catch (ArgumentException ex)
            {
                // Return a 404 Not Found response with the error message
                return NotFound(new {message = ex.Message});
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error with the error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }
    }
}