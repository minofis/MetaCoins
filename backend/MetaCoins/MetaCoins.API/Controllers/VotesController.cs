using AutoMapper;
using MetaCoins.API.Dtos.VotingDtos;
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
        [HttpGet("active-daily-session")]
        public async Task<ActionResult<DailySessionResponseDto>> GetActiveDailySession()
        {
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is empty
            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Get the active daily session
                var dailySession = await _votesService.GetActiveDailySessionAsync();

                // Map the daily session to the response DTO
                var dailySessionResponseDto = _mapper.Map<DailySessionResponseDto>(dailySession);

                // Return a 200 Ok response with the daily session
                return Ok(dailySessionResponseDto);
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
        [HttpGet("active-weekly-session")]
        public async Task<ActionResult<WeeklySessionResponseDto>> GetActiveWeeklySession()
        {
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Get the active weekly session
                var weeklySession = await _votesService.GetActiveWeeklySessionAsync();

                // Map the weekly session to the response DTO
                var weeklySessionResponseDto = _mapper.Map<WeeklySessionResponseDto>(weeklySession);

                // Return a 200 Ok response with the weekly session
                return Ok(weeklySessionResponseDto);
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
        [HttpGet("daily-vote/{id}")]
        public async Task<ActionResult<DailyVoteResponseDto>> GetDailyVoteById(Guid id)
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
                // Get the daily vote by specified id
                var dailyVote = await _votesService.GetDailyVoteByIdAsync(id);

                // Map the daily vote to the response DTO
                var dailyVoteResponseDto = _mapper.Map<DailyVoteResponseDto>(dailyVote);

                // Return a 200 Ok response with the daily vote
                return Ok(dailyVoteResponseDto);
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
        [HttpGet("weekly-session/{id}")]
        public async Task<ActionResult<WeeklySessionResponseDto>> GetWeeklySessionById(Guid id)
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
                // Get the weekly session by specified id
                var weeklySession = await _votesService.GetWeeklySessionByIdAsync(id);

                // Map the weekly session to the response DTO
                var weeklySessionResponseDto = _mapper.Map<WeeklySessionResponseDto>(weeklySession);

                // Return a 200 Ok response with the weekly session
                return Ok(weeklySessionResponseDto);
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
        [HttpGet("daily-session/{id}")]
        public async Task<ActionResult<DailySessionResponseDto>> GetDailySessionByStartDate(Guid id)
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
                // Get the daily session by specified id
                var dailySession = await _votesService.GetDailySessionByIdAsync(id);

                // Map the daily session to the response DTO
                var dailySessionResponseDto = _mapper.Map<DailySessionResponseDto>(dailySession);

                // Return a 200 Ok response with the daily session
                return Ok(dailySessionResponseDto);
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
        [HttpPost("vote-coin")]
        public async Task<IActionResult> VoteCoin([FromQuery]Guid coinId, [FromQuery]Guid dailySessionId)
        {
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Vote coin
                await _votesService.VoteCoinAsync(dailySessionId, userId, coinId);

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
        [HttpDelete("unvote-coin")]
        public async Task<IActionResult> UnvoteCoin([FromQuery]Guid coinId, [FromQuery]Guid dailySessionId)
        {
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Unvote coin
                await _votesService.UnvoteCoinAsync(dailySessionId, userId, coinId);

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
        public async Task<IActionResult> IsCoinVoted(Guid dailySessionId, Guid coinId)
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
                var isVoted = await _votesService.IsCoinVotedAsync(dailySessionId, userId, coinId);

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