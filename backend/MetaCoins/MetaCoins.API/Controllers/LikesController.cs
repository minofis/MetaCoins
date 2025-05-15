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
        private readonly IUsersService _usersService;
        public LikesController(ILikesService likesService, IUsersService usersService)
        {
            _likesService = likesService;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<List<CoinResponseDto>>> GetUserLikesByUsername(string username)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var likedCoins = await _likesService.GetLikedCoinsByUsernameAsync(username);

                var likedCoinResponseDtos = "Not implemented";

                return Ok(likedCoinResponseDtos);
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpPost("like-coin/{coinId}")]
        public async Task<IActionResult> LikeCoin(Guid coinId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                await _likesService.LikeCoinAsync(userId, coinId);

                return Created();
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpDelete("unlike-coin/{coinId}")]
        public async Task<IActionResult> UnlikeCoin(Guid coinId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                await _likesService.UnlikeCoinAsync(userId, coinId);

                return NoContent();
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("is-liked/{coinId}")]
        public async Task<IActionResult> IsCoinLiked(Guid coinId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var isLiked = await _likesService.IsCoinLikedAsync(userId, coinId);

                return Ok(isLiked);
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
    }
}