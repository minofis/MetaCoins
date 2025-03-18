using Microsoft.AspNetCore.Identity;

namespace MetaCoins.Core.Entities.Identity
{
    public class UserEntity : IdentityUser<Guid>
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}