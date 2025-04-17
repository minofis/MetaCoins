using MetaCoins.Core.Entities.Lookups.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder
                .HasData(
                    new TransactionType{Id = 1, Name = "FundsTransfer"},
                    new TransactionType{Id = 2, Name = "Charity"},
                    new TransactionType{Id = 3, Name = "Salary"},
                    new TransactionType{Id = 4, Name = "Topup"},
                    new TransactionType{Id = 5, Name = "MobileService"}
                );
        }
    }
}