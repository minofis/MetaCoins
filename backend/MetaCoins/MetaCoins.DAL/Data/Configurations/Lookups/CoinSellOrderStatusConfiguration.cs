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
                    new CoinSellOrderStatus{Id = 1, Name = "Active"},
                    new CoinSellOrderStatus{Id = 2, Name = "Completed"},
                    new CoinSellOrderStatus{Id = 3, Name = "Cancelled"}
                );
        }
    }
}