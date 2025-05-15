using MetaCoins.API.Dtos.CoinDtos;
using MetaCoins.Core.Entities.Helpers;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ICoinsService _coinsService;
        private readonly IConfiguration _config;
        public CoinsController(IUsersService usersService, ICoinsService coinsService, IConfiguration config)
        {
            _usersService = usersService;
            _coinsService = coinsService;
            _config = config;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<PaginatedResult<CoinResponseDto>>>> GetCoinsByQuery([FromQuery] CoinQueryObject query)
        {
                var paginatedCoins = await _coinsService.GetCoinsByQueryAsync(query);

                var paginatedDto = new PaginatedResult<CoinResponseDto>
                {
                    Items = paginatedCoins.Items.Select(i => new CoinResponseDto
                    {
                        Id = i.Id,
                        ImageUrl = _config["ApiUrl"] + i.ImageUrl,
                        Status = i.CoinStatus.Name,
                        Prompt = i.Prompt,
                        Title = i.Title,
                        Description = i.Description,
                        LikesCount = i.LikesCount,
                        OwnerUsername = i.Wallet.User.UserName,
                        CreatorUsername = i.Creator.User.UserName,
                        CreatedAt = i.CreatedAt.ToString()
                    }).ToList(),
                    TotalItems = paginatedCoins.TotalItems,
                    Page = paginatedCoins.Page,
                    PageSize = paginatedCoins.PageSize
                };

                return Ok(paginatedDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoinResponseDto>> GetCoinById(Guid id)
        {
            try
            {
                var coin = await _coinsService.GetCoinByIdAsync(id);

                var coinDto = new CoinResponseDto
                    {
                        Id = coin.Id,
                        ImageUrl = _config["ApiUrl"] + coin.ImageUrl,
                        Status = coin.CoinStatus.Name,
                        Prompt = coin.Prompt,
                        Title = coin.Title,
                        Description = coin.Description,
                        LikesCount = coin.LikesCount,
                        OwnerUsername = coin.Wallet.User.UserName,
                        CreatorUsername = coin.Creator.User.UserName,
                        CreatedAt = coin.CreatedAt.ToString()
                    };

                return Ok(coinDto);
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

        [Authorize(Policy = "CustomerPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateCoin(CoinCreateRequestDto requestDto)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated.");
            }
            try
            {
                await _coinsService.CreateCoinAsync(userId, requestDto.Prompt);

                return Ok(new {message = "Coin is created"});
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


        [Authorize(Policy = "CustomerPolicy")]
        [HttpGet("{id}/owner-records")]
        public async Task<ActionResult<List<CoinOwnerRecordResponseDto>>> GetOwnershipRecordsByCoinId(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var ownerRecords = await _coinsService.GetOwnerRecordsByCoinIdAsync(id);

                var ownerRecordDtos = ownerRecords.Select(or => new CoinOwnerRecordResponseDto
                {
                    Id = or.Id,
                    OwnerUsername = or.Wallet.User.UserName,
                    CoinId = or.CoinId,
                    AcquiredAt = or.AcquiredAt.ToString()
                }).ToList();

                return Ok(ownerRecordDtos);
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

        [Authorize(Policy = "CustomerPolicy")]
        [HttpPut("{id}/details")]
        public async Task<IActionResult> UpdateCoinDetails(Guid id, CoinUpdateDetailsRequestDto requestDto)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated.");
            }
            try
            {
                await _coinsService.UpdateCoinDetailsAsync(userId, id, requestDto.Title, requestDto.Description);

                return Ok(new {message = "Coin details are updated"});
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

        [Authorize(Policy = "CustomerPolicy")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateCoinStatus(Guid id, string status)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated.");
            }
            try
            {
                await _coinsService.UpdateCoinStatusAsync(userId, id, status);

                return Ok(new {message = "Coin status is updated"});
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