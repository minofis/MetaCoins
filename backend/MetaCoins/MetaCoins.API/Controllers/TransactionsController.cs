using AutoMapper;
using MetaCoins.API.Dtos.TransactionDtos;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionsService _transactionsService;
        private readonly IUsersService _usersService;
        public TransactionsController(ITransactionsService transactionsService, IUsersService usersService, IMapper mapper)
        {
            _transactionsService = transactionsService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionResponseDto>>> GetAllTransactions()
        {
            // Get all transactions
            var transactions = await _transactionsService.GetAllTransactionsAsync();

            // Map the transactions to a list of response DTOs
            var transactionDtos = _mapper.Map<List<TransactionResponseDto>>(transactions);

            // Return a 200 Ok response with the list of transactions
            return Ok(transactionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResponseDto>> GetTransaction(Guid id)
        {
            try
            {
                // Get transaction by the specified ID
                var transaction = await _transactionsService.GetTransactionByIdAsync(id);

                // Map the transaction to response DTO
                var transactionDto = _mapper.Map<TransactionResponseDto>(transaction);

                // Return a 200 Ok response with the transaction
                return Ok(transactionDto);
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

        [HttpPost("transfer-coin")]
        public async Task<ActionResult> TransferCoin([FromBody]TransactionCreateRequestDto transactionDto)
        {
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == null) return Unauthorized("User isn't authenticated");
            
            // Validate the incomming request
            if (transactionDto == null) return BadRequest("Transaction data is required");

            try
            {
                // Coins transfer service method
                await _transactionsService.TransferCoinAsync(transactionDto.SenderUsername, transactionDto.RecipientUsername, transactionDto.CoinId);

                // Return a 200 Ok response
                return Ok(new {message = "Coin transfer successful."});
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
    }
}