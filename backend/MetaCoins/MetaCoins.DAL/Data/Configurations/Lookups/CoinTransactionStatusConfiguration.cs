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
                    new CoinTransactionStatus{Id = 1, Name = "Pending"},
                    new CoinTransactionStatus{Id = 2, Name = "Completed"},
                    new CoinTransactionStatus{Id = 3, Name = "Failed"}
                );
        }
    }
}