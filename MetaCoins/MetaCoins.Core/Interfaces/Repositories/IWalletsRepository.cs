using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Helpers;

namespace MetaCoins.Core.Interfaces.Repositories
{
    public interface IWalletsRepository
    {
        Task<List<Wallet>> GetAllWalletsAsync();
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet> GetWalletByUsernameAsync(string username);
        Task<List<Wallet>> GetWalletsByFilterAsync(WalletsFilterDto filterDto);
        Task CreateWalletAsync(Wallet wallet);
        Task<int> GetWalletStatusId(string name);
        Task UpdateWalletStatusByIdAsync(Guid walletId, int statusId);
        Task SaveChangesAsync();
    }
}