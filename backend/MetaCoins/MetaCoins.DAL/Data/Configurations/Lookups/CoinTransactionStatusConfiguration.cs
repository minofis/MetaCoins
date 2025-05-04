using MetaCoins.Core.Entities.Enums.CoinTransaction;
using MetaCoins.Core.Entities.Lookups.CoinTransaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class CoinTransactionStatusConfiguration : IEntityTypeConfiguration<CoinTransactionStatus>
    {
        public void Configure(EntityTypeBuilder<CoinTransactionStatus> builder)
        {
            builder
                .HasData(
                    Enum.GetValues(typeof(CoinTransactionStatuses))
                        .Cast<CoinTransactionStatuses>()
                        .Select(e => new CoinTransactionStatus
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                );
        }
    }
}