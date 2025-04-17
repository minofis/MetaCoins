using AutoMapper;
using MetaCoins.API.Dtos.UserDtos;
using MetaCoins.API.Dtos.WalletDtos;
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
        private readonly IMapper _mapper;
        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            // Get all users
            var users = await _usersService.GetAllUsersAsync();

            // Map the users to a list of response DTOs
            var userResponseDtos = _mapper.Map<List<UserResponseDto>>(users);

            // Return a 200 Ok response with the list of users
            return Ok(userResponseDtos);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(Guid id)
        {
            try
            {
                // Get user by specified ID
                var user = await _usersService.GetUserByIdAsync(id);

                // Map user to user response DTO
                var userDto = _mapper.Map<UserResponseDto>(user);

                // Return a 200 Ok response with user DTO
                return Ok(userDto);
            }
            catch (ArgumentException ex)
            {
                // Return a 404 Not Found response with the error message
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error with the error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLoginRequestDto loginDto)
        {
            // Validate the incomming request
            if (loginDto == null)
            {
                return BadRequest("Login data is required");
            }
            try
            {
                // Login action
                var token = await _usersService.Login(loginDto.Username, loginDto.Password);

                Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                // Return a 200 OK
                return Ok(new {username = loginDto.Username});
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequestDto registerDto)
        {
            // Validate the incomming request
            if (registerDto == null)
            {
                return BadRequest("Register data is required");
            }
            try
            {
                // Register a new user
                await _usersService.Register
                (
                    registerDto.Username,
                    registerDto.Email, 
                    registerDto.Password
                );

                // Return a 201 Created 
                return Created();
            }
            catch (ArgumentException ex)
            {
                // Return a 400 Bad Request response with the error message
                return BadRequest(new {message = ex.Message});
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error with the error message
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
                // Return a 404 Not Found response with the error message
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error with the error message
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
                // Return a 404 Not Found response with the error message
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error with the error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }
    }
}