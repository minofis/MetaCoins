using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IWalletsService
    {
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet> GetWalletByUsernameAsync(string username);
        Task<Wallet> GetWalletByUserIdAsync(Guid userId);
        Task CreateWalletAsync(Guid walletId, Guid userId);
    }
}