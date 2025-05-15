using MetaCoins.API.Dtos.CoinSellOrderDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class CoinSellOrdersController : ControllerBase
    {
        private readonly ICoinSellOrdersService _coinSellOrdersService;
        private readonly IUsersService _usersService;
        public CoinSellOrdersController(ICoinSellOrdersService coinSellOrdersService, IUsersService usersService)
        {
            _coinSellOrdersService = coinSellOrdersService;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CoinSellOrderResponseDto>> GetCoinSellOrder(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var coinSellOrder = await _coinSellOrdersService.GetCoinSellOrderById(id);

                var coinSellOrderDto = new CoinSellOrderResponseDto
                {
                    Id = coinSellOrder.Id,
                    Status = coinSellOrder.Status.Name,
                    CoinId = coinSellOrder.CoinId,
                    SellerWalletId = coinSellOrder.SellerWalletId,
                    Price = coinSellOrder.Price,
                    CreatedAt = coinSellOrder.CreatedAt.ToString(),
                    UpdatedAt = coinSellOrder.UpdatedAt.ToString(),
                    CompletedAt = coinSellOrder.CompletedAt.ToString(),
                };
                
                return Ok(coinSellOrderDto);
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
        public async Task<ActionResult> Create([FromBody]CoinSellOrderRequestDto requestDto)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            } 
            
            if (requestDto == null) 
                return BadRequest("Coin sell order data is required");

            try
            {
                await _coinSellOrdersService.CreateCoinSellOrderAsync(requestDto.CoinId, userId, requestDto.Price);

                return Ok(new {message = "Coin sell order is created."});
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
        public async Task<IActionResult> UpdateCoinSellOrderStatus(Guid id, string status)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated.");
            }
            try
            {
                await _coinSellOrdersService.UpdateCoinSellOrderStatusAsync(id, userId, status);

                return Ok(new {message = "Coin sell order status is updated."});
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