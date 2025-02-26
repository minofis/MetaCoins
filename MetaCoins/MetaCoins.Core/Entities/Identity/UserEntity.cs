using Microsoft.AspNetCore.Identity;

namespace MetaCoins.Core.Entities.Identity
{
    public class UserEntity : IdentityUser<Guid>
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}