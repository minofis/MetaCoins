using MetaCoins.Core.Entities.Lookups.Coin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class CoinStatusConfiguration : IEntityTypeConfiguration<CoinStatus>
    {
        public void Configure(EntityTypeBuilder<CoinStatus> builder)
        {
            builder
                .HasData(
                    new CoinStatus{Id = 1, Name = "Draft"},
                    new CoinStatus{Id = 2, Name = "Public"},
                    new CoinStatus{Id = 3, Name = "Deleted"}
                );
        }
    }
}