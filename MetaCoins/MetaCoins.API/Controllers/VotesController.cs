using AutoMapper;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService _votesService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        public VotesController(IVotesService votesService, IUsersService usersService, IMapper mapper)
        {
            _votesService = votesService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpPost("vote-coin/{coinId}")]
        public async Task<IActionResult> VoteCoin(Guid coinId)
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
                // Vote coin
                await _votesService.VoteCoinAsync(userId, coinId);

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
        [HttpDelete("unvote-coin/{coinId}")]
        public async Task<IActionResult> UnvoteCoin(Guid coinId)
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
                // Unvote coin
                await _votesService.UnvoteCoinAsync(userId, coinId);

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
        [HttpGet("is-voted/{coinId}")]
        public async Task<IActionResult> IsCoinVoted(Guid coinId)
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
                // Check is coin voted
                var isVoted = await _votesService.IsCoinVotedAsync(userId, coinId);

                // Return a 201 Created 
                return Ok(isVoted);
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