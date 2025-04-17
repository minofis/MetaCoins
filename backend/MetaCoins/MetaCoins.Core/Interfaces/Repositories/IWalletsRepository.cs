using MetaCoins.Core.Entities;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IWalletsRepository
    {
        Task<List<Wallet>> GetAllWalletsAsync();
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet> GetWalletByUsernameAsync(string username);
        Task CreateWalletAsync(Wallet wallet);
        Task SaveChangesAsync();
    }
}