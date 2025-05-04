using AutoMapper;
using MetaCoins.API.Dtos.CoinTransactionDtos;
using MetaCoins.API.Dtos.WalletDtos;
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

            // Check if userId is empty
            if (userId == Guid.Empty)
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

            // Check if userId is empty
            if (userId == Guid.Empty)
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

            // Check if userId is empty
            if (userId == Guid.Empty)
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


        [Authorize(Policy = "CustomerPolicy")]
        [HttpGet("{id}/sent-transactions")]
        public async Task<ActionResult<List<CoinTransactionResponseDto>>> GetSentTransactions(Guid id)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                if (wallet.UserId != userId)
                {
                    return Forbid("Bearer");
                }

                var coinTransactions = await _walletsService.GetSentTransactionsByIdAsync(id);

                var coinTransactionDtos = coinTransactions.Select(ct => new CoinTransactionResponseDto
                {
                    Id = ct.Id,
                    Type = ct.Type.Name,
                    Status = ct.Status.Name,
                    CoinId = ct.CoinId,
                    SenderWalletId = ct.SenderWalletId,
                    RecipientWalletId = ct.RecipientWalletId,
                    CoinSellOrderId = ct.CoinSellOrderId,
                    CreatedAt = ct.CreatedAt.ToString(),
                    UpdatedAt = ct.UpdatedAt.ToString(),
                    CompletedAt = ct.CompletedAt.ToString(),
                }).ToList();

                return Ok(coinTransactionDtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }

        [Authorize(Policy = "CustomerPolicy")]
        [HttpGet("{id}/recived-transactions")]
        public async Task<ActionResult<List<CoinTransactionResponseDto>>> GetRecivedTransactions(Guid id)
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
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                if (wallet.UserId != userId)
                {
                    return Forbid("Bearer");
                }

                var coinTransactions = await _walletsService.GetRecivedTransactionsByIdAsync(id);

                var coinTransactionDtos = coinTransactions.Select(ct => new CoinTransactionResponseDto
                {
                    Id = ct.Id,
                    Type = ct.Type.Name,
                    Status = ct.Status.Name,
                    CoinId = ct.CoinId,
                    SenderWalletId = ct.SenderWalletId,
                    RecipientWalletId = ct.RecipientWalletId,
                    CoinSellOrderId = ct.CoinSellOrderId,
                    CreatedAt = ct.CreatedAt.ToString(),
                    UpdatedAt = ct.UpdatedAt.ToString(),
                    CompletedAt = ct.CompletedAt.ToString(),
                }).ToList();

                return Ok(coinTransactionDtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            };
        }
    }
}