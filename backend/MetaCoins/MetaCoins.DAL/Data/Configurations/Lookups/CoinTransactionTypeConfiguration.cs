using MetaCoins.Core.Entities.Lookups.CoinTransaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class CoinTransactionTypeConfiguration : IEntityTypeConfiguration<CoinTransactionType>
    {
        public void Configure(EntityTypeBuilder<CoinTransactionType> builder)
        {
            builder
                .HasData(
                    new CoinTransactionType{Id = 1, Name = "Transfer"},
                    new CoinTransactionType{Id = 2, Name = "Sale"}
                );
        }
    }
}