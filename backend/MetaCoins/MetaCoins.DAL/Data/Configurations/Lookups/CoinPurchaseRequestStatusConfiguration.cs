using MetaCoins.Core.Entities.Enums;
using MetaCoins.Core.Entities.Lookups.CoinPurchaseRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class CoinPurchaseRequestStatusConfiguration : IEntityTypeConfiguration<CoinPurchaseRequestStatus>
    {
        public void Configure(EntityTypeBuilder<CoinPurchaseRequestStatus> builder)
        {
            builder
                .HasData(
                    Enum.GetValues(typeof(CoinPurchaseRequestStatuses))
                        .Cast<CoinPurchaseRequestStatuses>()
                        .Select(e => new CoinPurchaseRequestStatus
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                );
        }
    }
}