using MetaCoins.Core.Entities.Enums.CoinTransaction;
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
                    Enum.GetValues(typeof(CoinTransactionTypes))
                        .Cast<CoinTransactionTypes>()
                        .Select(e => new CoinTransactionType
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                );
        }
    }
}