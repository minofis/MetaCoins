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
        private readonly IWalletsService _walletsService;
        private readonly IUsersService _usersService;
        public WalletsController(IWalletsService walletsService, IUsersService usersService)
        {
            _walletsService = walletsService;
            _usersService = usersService;
        }

        [Authorize(Policy = "AdminOrCustomerPolicy")]
        [HttpGet("my-wallet")]
        public async Task<ActionResult<WalletResponseDto>> GetMyWallet()
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }

            var isUserExists = await _usersService.UserExistsAsync(userId);

            if (!isUserExists)
            {
                return Unauthorized("User isn't authenticated");
            }

            try
            {
                var wallet = await _walletsService.GetWalletByUserIdAsync(userId);

                var walletResponseDto = new WalletResponseDto
                {
                    Id = wallet.Id,
                    OwnerUsername = wallet.User.UserName,
                    Balance = wallet.Balance,
                    CreatedAt = wallet.CreatedAt.ToString()
                };

                return Ok(walletResponseDto);
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
        public async Task<ActionResult<WalletResponseDto>> GetWalletById(Guid id)
        {
            if (User.IsInRole("Admin"))
            {
                var wallet = await _walletsService.GetWalletByIdAsync(id);

                var walletResponseDto = new WalletResponseDto
                {
                    Id = wallet.Id,
                    OwnerUsername = wallet.User.UserName,
                    Balance = wallet.Balance,
                    CreatedAt = wallet.CreatedAt.ToString()
                };

                return Ok(walletResponseDto);
            }

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

                var walletResponseDto = new WalletResponseDto
                {
                    Id = wallet.Id,
                    OwnerUsername = wallet.User.UserName,
                    Balance = wallet.Balance,
                    CreatedAt = wallet.CreatedAt.ToString()
                };

                return Ok(walletResponseDto);
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
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<WalletResponseDto>> GetWalletByUsername(string username)
        {
            var userId = await _usersService.GetCurrentUserIdAsync();

            if (userId == Guid.Empty)
            {
                return Unauthorized("User isn't authenticated");
            }
            try
            {
                var wallet = await _walletsService.GetWalletByUsernameAsync(username);

                var walletResponseDto = new WalletResponseDto
                {
                    Id = wallet.Id,
                    OwnerUsername = wallet.User.UserName,
                    Balance = wallet.Balance,
                    CreatedAt = wallet.CreatedAt.ToString()
                };

                return Ok(walletResponseDto);
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