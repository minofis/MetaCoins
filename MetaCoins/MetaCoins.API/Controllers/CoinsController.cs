using AutoMapper;
using MetaCoins.API.Dtos.CoinDtos;
using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly ICoinsService _coinsService;
        public CoinsController(IMapper mapper, IUsersService usersService, ICoinsService coinsService)
        {
            _mapper = mapper;
            _usersService = usersService;
            _coinsService = coinsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CoinResponseDto>>> GetAllCoins()
        {
            // Get all coins
            var coins = await _coinsService.GetAllCoinsAsync();

            // Map the coins to a list of response DTOs
            var coinDtos = _mapper.Map<List<CoinResponseDto>>(coins);

            // Return a 200 Ok response with the list of coins
            return Ok(coinDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoinResponseDto>> GetCoin(Guid id)
        {
            try
            {
                // Get coin by the specified ID
                var coin = await _coinsService.GetCoinByIdAsync(id);

                // Map the coin to response DTO
                var coinDto = _mapper.Map<CoinResponseDto>(coin);

                // Return a 200 Ok response with the coin
                return Ok(coinDto);
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

        [Authorize(Policy = "CustomerPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateCoinByUsername([FromBody] CoinCreateRequestDto coinDto)
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
                // Create a coin
                await _coinsService.CreateCoinAsync(coinDto.Username);

                // Return a 201 Created response
                return Created();
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


        [Authorize(Policy = "CustomerPolicy")]
        [HttpGet("{id}/ownership-records")]
        public async Task<ActionResult<List<CoinOwnerRecord>>> GetCoinOwnershipRecordsById(Guid id)
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
                var ownerRecords = await _coinsService.GetCoinOwnershipRecordsByIdAsync(id);

                var ownerRecordDtos = _mapper.Map<List<CoinOwnerRecordResponseDto>>(ownerRecords);

                // Return a Ok response
                return Ok(ownerRecordDtos);
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