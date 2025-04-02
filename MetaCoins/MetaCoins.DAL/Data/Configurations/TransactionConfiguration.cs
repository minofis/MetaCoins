using MetaCoins.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasOne(t => t.SenderWallet)
                .WithMany(w => w.SentTransactions)
                .HasForeignKey(t => t.SenderWalletId);

            builder
                .HasOne(t => t.RecipientWallet)
                .WithMany(w => w.RecivedTransactions)
                .HasForeignKey(t => t.RecipientWalletId);

            builder
                .HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId);

            builder
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);
        }
    }
}