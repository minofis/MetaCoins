using MetaCoins.Core.Entities.Lookups.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Lookups
{
    public class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder
                .HasData(
                    new TransactionStatus{Id = 1, Name = "Pending"},
                    new TransactionStatus{Id = 2, Name = "Completed"},
                    new TransactionStatus{Id = 3, Name = "Failed"}
                );
        }
    }
}