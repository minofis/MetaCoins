using MetaCoins.API.Dtos.UserDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsersAsync();

            var userResponseDtos = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email
            }).ToList();

            return Ok(userResponseDtos);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(Guid id)
        {
            try
            {
                var user = await _usersService.GetUserByIdAsync(id);

                var userDto = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email
                };

                return Ok(userDto);
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLoginRequestDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Login data is required");
            }
            try
            {
                var token = await _usersService.Login(loginDto.Username, loginDto.Password);

                Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok(new {message = "Login is successful"});
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequestDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("Register data is required");
            }
            try
            {
                await _usersService.Register
                (
                    registerDto.Username,
                    registerDto.Email, 
                    registerDto.Password
                );

                return Ok(new {message = "User is registered"});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("{id}/add-role")]
        public async Task<IActionResult> AddRoleToUser([FromQuery]string roleName, Guid id)
        {
            try
            {
                await _usersService.AssignRoleToUserAsync(id, roleName);

                return Ok();
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

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("{id}/remove-role")]
        public async Task<IActionResult> RemoveRoleFromUser([FromQuery]string roleName, Guid id)
        {
            try
            {
                await _usersService.RemoveRoleFromUserAsync(id, roleName);

                return Ok();
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