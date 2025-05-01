using MetaCoins.API.Dtos.VotingDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class VotingSessionsController : ControllerBase
    {
        private readonly IVotingSessionsService _votingSessionsService;
        private readonly IUsersService _usersService;
        public VotingSessionsController(IVotingSessionsService votingSessionsService, IUsersService usersService)
        {
            _usersService = usersService;
            _votingSessionsService = votingSessionsService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("active-sessions")]
        public async Task<ActionResult<List<VotingSessionResponseDto>>> GetActiveVotingSessions()
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var votingSessions = await _votingSessionsService.GetActiveVotingSessionsAsync();

                var votingSessionResponseDtos = votingSessions.Select(vs => new VotingSessionResponseDto
                {
                    Id = vs.Id,
                    Title = vs.Title,
                    VotingType = vs.VotingType.Name,
                    IsActive = vs.IsActive,
                    StartDate = vs.StartDate.ToString(),
                    EndDate = vs.EndDate.ToString(),
                    ParentVotingSessionId = vs.ParentVotingSessionId,
                    SubVotingSessionIds = vs.SubVotingSessions.Select(svs => svs.Id).ToArray(),
                    VoteCoinIds = vs.CoinVotingSessions.Select(cvs => cvs.CoinId).ToArray(),
                    WinnerId = vs.WinnerId
                }).ToList();

                return Ok(votingSessionResponseDtos);
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
        [HttpGet("{id}")]
        public async Task<ActionResult<VotingSessionResponseDto>> GetVotingSessionById(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var votingSession = await _votingSessionsService.GetVotingSessionByIdAsync(id);

                var votingSessionResponseDto = new VotingSessionResponseDto
                {
                    Id = votingSession.Id,
                    Title = votingSession.Title,
                    VotingType = votingSession.VotingType.Name,
                    IsActive = votingSession.IsActive,
                    StartDate = votingSession.StartDate.ToString(),
                    EndDate = votingSession.EndDate.ToString(),
                    ParentVotingSessionId = votingSession.ParentVotingSessionId,
                    SubVotingSessionIds = votingSession.SubVotingSessions.Select(svs => svs.Id).ToArray(),
                    VoteCoinIds = votingSession.CoinVotingSessions.Select(cvs => cvs.CoinId).ToArray(),
                    WinnerId = votingSession.WinnerId
                };

                return Ok(votingSessionResponseDto);
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
        [HttpPost]
        public async Task<IActionResult> CreateVotingSession(VotingSessionRequestDto requestDto)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }

            if (requestDto == null)
            {
                return BadRequest("Voting session data is required");
            }
            try
            {
                await _votingSessionsService.CreateVotingSessionAsync(
                    requestDto.Title,
                    requestDto.VotingTypeId,
                    requestDto.EndDate,
                    requestDto.CoinIds,
                    requestDto.ParentVotingSessionId
                );

                return Ok("Voting session is created");
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
        [HttpPost("determine-winner/{id}")]
        public async Task<IActionResult> DetermineWinner(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                await _votingSessionsService.DetermineWinnerAsync(id);

                return Ok("Voting session winner is determined");
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
        [HttpPut("deactivate-session/{id}")]
        public async Task<IActionResult> DeactivateVotingSession(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                await _votingSessionsService.DeactivateVotingSessionAsync(id);

                return Ok("Voting session is deactivated");
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