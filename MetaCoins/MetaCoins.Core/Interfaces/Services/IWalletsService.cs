using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Services
{
    public interface IWalletsService
    {
        Task<List<Wallet>> GetAllWalletsAsync();
        Task<List<Wallet>> GetWalletsByFilterAsync(string? ownerUsername, string? status, string? type);
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<WalletDetails> GetWalletDetailsByIdAsync(Guid walletId);
        Task<List<Coin>> GetWalletCoinsByIdAsync(Guid walletId);
        Task<List<Transaction>> GetRecivedTransactionsByIdAsync(Guid walletId);
        Task<List<Transaction>> GetSentTransactionsByIdAsync(Guid walletId);
        Task CreateWalletAsync(Guid userId, string type);
        Task UpdateWalletStatusByIdAsync(Guid walletId, string status);
    }
}