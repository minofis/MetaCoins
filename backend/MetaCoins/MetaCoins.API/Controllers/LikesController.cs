using AutoMapper;
using MetaCoins.API.Dtos.CoinDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        public LikesController(ILikesService likesService, IUsersService usersService, IMapper mapper)
        {
            _likesService = likesService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<List<CoinResponseDto>>> GetUserLikesByUsername(string username)
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
                // Get the likes
                var likedCoins = await _likesService.GetLikedCoinsByUsernameAsync(username);

                // Map the likes to the response DTOs
                var likedCoinResponseDtos = _mapper.Map<List<CoinResponseDto>>(likedCoins);

                // Return a 200 Ok response with the likes
                return Ok(likedCoinResponseDtos);
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpPost("like-coin/{coinId}")]
        public async Task<IActionResult> LikeCoin(Guid coinId)
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
                // Like coin
                await _likesService.LikeCoinAsync(userId, coinId);

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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpDelete("unlike-coin/{coinId}")]
        public async Task<IActionResult> UnlikeCoin(Guid coinId)
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
                // Unlike coin
                await _likesService.UnlikeCoinAsync(userId, coinId);

                // Return a 204 NoContent 
                return NoContent();
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("is-liked/{coinId}")]
        public async Task<IActionResult> IsCoinLiked(Guid coinId)
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
                // Check is coin liked
                var isLiked = await _likesService.IsCoinLikedAsync(userId, coinId);

                // Return a 201 Created 
                return Ok(isLiked);
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
    }
}