using MetaCoins.API.Dtos.VotingDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class CoinVotesController : ControllerBase
    {
        private readonly ICoinVotesService _coinVotesService;
        private readonly IUsersService _usersService;
        public CoinVotesController(ICoinVotesService coinVotesService, IUsersService usersService)
        {
            _usersService = usersService;
            _coinVotesService = coinVotesService;
        }

        

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-session/{sessionId}")]
        public async Task<ActionResult<List<CoinVoteResponseDto>>> GetCoinVotesByVotingSessionId(Guid sessionId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var coinVotes = await _coinVotesService.GetCoinVotesByVotingSessionIdAsync(sessionId);

                var coinVoteDtos = coinVotes.Select(
                    coinVote => new CoinVoteResponseDto
                    {
                        Id = coinVote.Id,
                        UserId = coinVote.UserId,
                        CoinId = coinVote.CoinId,
                        VotingSessionId = coinVote.VotingSessionId,
                        CreatedAt = coinVote.CreatedAt.ToString()
                    }
                );

                return Ok(coinVoteDtos);
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
        [HttpPost("vote-coin")]
        public async Task<IActionResult> VoteCoin([FromQuery]Guid coinId, [FromQuery]Guid votingSessionId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                await _coinVotesService.VoteCoinAsync(votingSessionId, userId, coinId);

                return Ok("Voted successfully");
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
        [HttpDelete("unvote-coin")]
        public async Task<IActionResult> UnvoteCoin([FromQuery]Guid coinId, [FromQuery]Guid votingSessionId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                await _coinVotesService.UnvoteCoinAsync(votingSessionId, userId, coinId);

                return Ok("Unvoted successfully");
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
        [HttpGet("is-voted/{coinId}")]
        public async Task<IActionResult> IsCoinVoted(Guid votingSessionId, Guid coinId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var isVoted = await _coinVotesService.IsCoinVotedAsync(votingSessionId, userId, coinId);

                return Ok(isVoted);
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