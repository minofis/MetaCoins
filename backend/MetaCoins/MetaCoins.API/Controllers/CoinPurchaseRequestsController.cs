using MetaCoins.API.Dtos.CoinPurchaseRequestDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class CoinPurchaseRequestsController : ControllerBase
    {
        private readonly ICoinPurchaseRequestsService _coinPurchaseRequestsService;
        private readonly IUsersService _usersService;
        public CoinPurchaseRequestsController(ICoinPurchaseRequestsService coinPurchaseRequestsService, IUsersService usersService)
        {
            _coinPurchaseRequestsService = coinPurchaseRequestsService;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CoinPurchaseRequestResponseDto>> GetCoinPurchaseRequest(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var coinPurchaseRequest = await _coinPurchaseRequestsService.GetCoinPurchaseRequestByIdAsync(id);

                var coinPurchaseRequestDto = new CoinPurchaseRequestResponseDto
                {
                    Id = coinPurchaseRequest.Id,
                    Status = coinPurchaseRequest.Status.Name,
                    CoinSellOrderId = coinPurchaseRequest.CoinSellOrderId,
                    BuyerWalletId = coinPurchaseRequest.BuyerWalletId,
                    CreatedAt = coinPurchaseRequest.CreatedAt.ToString(),
                    RespondedAt = coinPurchaseRequest.RespondedAt.ToString(),
                };
                
                return Ok(coinPurchaseRequestDto);
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
        public async Task<ActionResult> Create(Guid coinSellOrderId)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            } 
            
            if (coinSellOrderId == Guid.Empty) 
                return BadRequest("Coin sell order ID is required");

            try
            {
                await _coinPurchaseRequestsService.CreateCoinPurchaseRequestAsync(coinSellOrderId, userId);

                return Ok(new {message = "Coin purchase request is created."});
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
        public async Task<IActionResult> UpdateCoinPurchaseRequestStatus(Guid id, string status)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated.");
            }
            try
            {
                await _coinPurchaseRequestsService.UpdateCoinPurchaseRequestStatusAsync(userId, id, status);

                return Ok(new {message = "Coin purchase request status is updated."});
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