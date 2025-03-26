using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IWalletsService
    {
        Task<List<Wallet>> GetAllWalletsAsync();
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet> GetWalletByUsernameAsync(string username);
        Task<List<Coin>> GetWalletCoinsByUsernameAsync(string username);
        Task<List<Coin>> GetWalletCoinsByIdAsync(Guid walletId);
        Task<List<Transaction>> GetRecivedTransactionsByIdAsync(Guid walletId);
        Task<List<Transaction>> GetSentTransactionsByIdAsync(Guid walletId);
        Task CreateWalletAsync(Guid walletId, Guid userId);
    }
}