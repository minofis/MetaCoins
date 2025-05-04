using MetaCoins.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class CoinTransactionConfiguration: IEntityTypeConfiguration<CoinTransaction>
    {
        public void Configure(EntityTypeBuilder<CoinTransaction> builder)
        {
            // CoinTransaction to SenderWallet
            builder
                .HasOne(ct => ct.SenderWallet)
                .WithMany(sw =>sw.SentTransactions)
                .HasForeignKey(ct => ct.SenderWalletId);

            // CoinTransaction to RecipientWallet
            builder
                .HasOne(ct => ct.RecipientWallet)
                .WithMany(rw => rw.RecivedTransactions)
                .HasForeignKey(ct => ct.RecipientWalletId);

            // CoinTransaction to CoinTransactionType
            builder
                .HasOne(ct => ct.Type)
                .WithMany()
                .HasForeignKey(ct => ct.TypeId);

            // CoinTransaction to CoinTransactionStatus
            builder
                .HasOne(ct => ct.Status)
                .WithMany()
                .HasForeignKey(ct => ct.StatusId);

            // CoinTransaction to Coin
            builder
                .HasOne(ct => ct.Coin)
                .WithMany()
                .HasForeignKey(ct => ct.CoinId);

            // CoinTransaction to CoinSellOrder
            builder
                .HasOne(ct => ct.CoinSellOrder)
                .WithMany()
                .HasForeignKey(ct => ct.CoinSellOrderId);
        }
    }
}