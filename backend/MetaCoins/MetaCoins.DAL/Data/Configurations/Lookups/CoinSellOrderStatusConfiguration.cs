using MetaCoins.Core.Entities.Enums.CoinSellOrder;
using MetaCoins.Core.Entities.Lookups.CoinSellOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class CoinSellOrderStatusConfiguration : IEntityTypeConfiguration<CoinSellOrderStatus>
    {
        public void Configure(EntityTypeBuilder<CoinSellOrderStatus> builder)
        {
            builder
                .HasData(
                    Enum.GetValues(typeof(CoinSellOrderStatuses))
                        .Cast<CoinSellOrderStatuses>()
                        .Select(e => new CoinSellOrderStatus
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                );
        }
    }
}