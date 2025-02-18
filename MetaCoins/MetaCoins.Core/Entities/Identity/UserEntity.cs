using Microsoft.AspNetCore.Identity;

namespace MetaCoins.Core.Entities.Identity
{
    public class UserEntity : IdentityUser<Guid>
    {
        public List<Wallet> Wallets { get; set; }
    }
}