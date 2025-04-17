using MetaCoins.Core.Entities.Identity;

namespace MetaCoins.Core.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}