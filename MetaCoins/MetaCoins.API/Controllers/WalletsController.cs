using AutoMapper;
using MetaCoins.API.Dtos.CoinDtos;
using MetaCoins.API.Dtos.TransactionDtos;
using MetaCoins.API.Dtos.WalletDtos;
using MetaCoins.Core.Entities;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaCoins.API.Controllers
{
    [ApiController]
    [Route("meta-coins/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalletsService _walletsService;
        private readonly IUsersService _usersService;
        public WalletsController(IWalletsService walletsService, IMapper mapper, IUsersService usersService)
        {
            _walletsService = walletsService;
            _mapper = mapper;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<WalletResponseDto>>> GetAllWallets()
        {
            if (User.IsInRole("Admin"))
            {
                // Get all wallets
                var wallets = await _walletsService.GetAllWalletsAsync();

                // Map the wallets to a list of response DTOs
                var walletResponseDtos = _mapper.Map<List<WalletResponseDto>>(wallets);

                // Return a 200 Ok response with the list of wallets
                return Ok(walletResponseDtos);
            }
            
            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == null)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Get user wallet
                var user = await _usersService.GetUserByIdAsync(userId);
                // Get user wallet
                var wallet = await _walletsService.GetWalletByUsernameAsync(user.UserName);

                // Map the wallet to a response DTO
                var walletResponseDto = _mapper.Map<WalletResponseDto>(wallet);

                // Return a 200 Ok response with the wallet
                return Ok(walletResponseDto);
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<WalletResponseDto>> GetWalletById(Guid id)
        {
            if (User.IsInRole("Admin"))
            {
                // Get wallet by the specified ID
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                // Map the wallet entity to response DTO
                var walletResponseDto = _mapper.Map<WalletResponseDto>(wallet);

                // Return a 200 Ok response with the wallet
                return Ok(walletResponseDto);
            }

            // Get user id from the current user
            var userId = await _usersService.GetCurrentUserIdAsync();

            // Check if userId is null
            if (userId == null)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                // Get wallet by the specified ID
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                if (wallet.UserId != userId)
                {
                    return Forbid("Bearer");
                }

                // Map the wallet entity to response DTO
                var walletResponseDto = _mapper.Map<WalletResponseDto>(wallet);

                // Return a 200 Ok response with the wallet
                return Ok(walletResponseDto);
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<WalletResponseDto>> GetWalletByUsername(string username)
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
                // Get wallet by the specified username
                var wallet = await _walletsService.GetWalletByUsernameAsync(username);

                // Map the wallet entity to response DTO
                var walletResponseDto = _mapper.Map<WalletResponseDto>(wallet);

                // Return a 200 Ok response with the wallet
                return Ok(walletResponseDto);
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

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("by-username/{username}/coins")]
        public async Task<ActionResult<CoinResponseDto>> GetWalletCoinsByUsername(string username)
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
                // Get wallet by the specified username
                var wallet = await _walletsService.GetWalletByUsernameAsync(username);

                // Get the wallet coins by the specified username
                var coins = await _walletsService.GetWalletCoinsByUsernameAsync(username);

                // Map the coins to response Dtos
                var coinDtos = _mapper.Map<List<CoinResponseDto>>(coins);

                // Return a Ok response with the list of coins
                return Ok(coinDtos);
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


        [Authorize(Policy = "CustomerPolicy")]
        [HttpGet("{id}/sent-transactions")]
        public async Task<ActionResult<List<Transaction>>> GetSentTransactions(Guid id)
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
                // Get wallet by the specified ID
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                if (wallet.UserId != userId)
                {
                    return Forbid("Bearer");
                }

                // Get sent transactions associated with the specified bank card ID
                var transactions = await _walletsService.GetSentTransactionsByIdAsync(id);

                // Map the transactions to a list of response DTOs
                var transactionDtos = _mapper.Map<List<TransactionResponseDto>>(transactions);

                // Return a 200 Ok response with the list of sent transactions
                return Ok(transactionDtos);
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
        [HttpGet("{id}/recived-transactions")]
        public async Task<ActionResult<List<Transaction>>> GetRecivedTransactions(Guid id)
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
                // Get wallet by the specified ID
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                if (wallet.UserId != userId)
                {
                    return Forbid("Bearer");
                }

                // Get recived transactions associated with the specified bank card ID
                var transactions = await _walletsService.GetRecivedTransactionsByIdAsync(id);

                // Map the transactions to a list of response DTOs
                var transactionDtos = _mapper.Map<List<TransactionResponseDto>>(transactions);

                // Return a 200 Ok response with the list of sent transactions
                return Ok(transactionDtos);
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

/*
        [Authorize(Policy = "CustomerPolicy")]
        [HttpGet("{id}/coins")]
        public async Task<ActionResult<CoinResponseDto>> GetWalletCoinsById(Guid id)
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
                // Get wallet by the specified ID
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                if (wallet.UserId != userId || wallet.Status.Name == "Closed")
                {
                    return Forbid("Bearer");
                }

                // Get the wallet coins by the specified ID
                var coins = await _walletsService.GetWalletCoinsByIdAsync(id);

                // Map the coins to response Dtos
                var coinDtos = _mapper.Map<List<CoinResponseDto>>(coins);

                // Return a Ok response with the list of coins
                return Ok(coinDtos);
            }
            catch (ArgumentException ex)
            {
                // Return a 404 Not Found response with the error message
                return NotFound(new {error = ex.Message});
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error with the error message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }
*/
}