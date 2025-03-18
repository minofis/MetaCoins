using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities
{
    public class Like
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}