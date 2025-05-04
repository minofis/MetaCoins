using MetaCoins.Core.Entities.Enums.Coin;
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
                    Enum.GetValues(typeof(CoinStatuses))
                        .Cast<CoinStatuses>()
                        .Select(e => new CoinStatus
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                );
        }
    }
}