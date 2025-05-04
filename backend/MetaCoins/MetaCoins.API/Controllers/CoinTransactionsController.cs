using MetaCoins.API.Dtos.CoinTransactionDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class CoinTransactionsController : ControllerBase
    {
        private readonly ICoinTransactionsService _coinTransactionsService;
        private readonly IUsersService _usersService;
        public CoinTransactionsController(ICoinTransactionsService coinTransactionsService, IUsersService usersService)
        {
            _coinTransactionsService = coinTransactionsService;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CoinTransactionResponseDto>> GetCoinTransaction(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var coinTransaction = await _coinTransactionsService.GetCoinTransactionById(id);

                var coinTransactionDto = new CoinTransactionResponseDto
                {
                    Id = coinTransaction.Id,
                    Type = coinTransaction.Type.Name,
                    Status = coinTransaction.Status.Name,
                    CoinId = coinTransaction.CoinId,
                    SenderWalletId = coinTransaction.SenderWalletId,
                    RecipientWalletId = coinTransaction.RecipientWalletId,
                    CoinSellOrderId = coinTransaction.CoinSellOrderId,
                    CreatedAt = coinTransaction.CreatedAt.ToString(),
                    UpdatedAt = coinTransaction.UpdatedAt.ToString(),
                    CompletedAt = coinTransaction.CompletedAt.ToString(),
                };
                
                return Ok(coinTransactionDto);
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
        [HttpPost("transfer-coin")]
        public async Task<ActionResult> TransferCoin([FromBody]TransferCoinRequestDto requestDto)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            } 
            
            if (requestDto == null) 
                return BadRequest("Transaction data is required");

            try
            {
                await _coinTransactionsService.TransferCoinAsync(userId, requestDto.RecipientUsername, requestDto.CoinId);

                return Ok(new {message = "Coin is transferred."});
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