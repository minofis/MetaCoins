using MetaCoins.Core.Entities.Votes;
using Microsoft.AspNetCore.Identity;

namespace MetaCoins.Core.Entities.Identity
{
    public class UserEntity : IdentityUser<Guid>
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<CoinVote> CoinVotes { get; set; } = new List<CoinVote>();
    }
}