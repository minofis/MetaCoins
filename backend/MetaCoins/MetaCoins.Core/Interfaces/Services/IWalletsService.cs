using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IWalletsService
    {
        Task<List<Wallet>> GetAllWalletsAsync();
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet> GetWalletByUsernameAsync(string username);
        Task<Wallet> GetWalletByUserIdAsync(Guid userId);
        Task<List<Transaction>> GetRecivedTransactionsByIdAsync(Guid walletId);
        Task<List<Transaction>> GetSentTransactionsByIdAsync(Guid walletId);
        Task CreateWalletAsync(Guid walletId, Guid userId);
    }
}