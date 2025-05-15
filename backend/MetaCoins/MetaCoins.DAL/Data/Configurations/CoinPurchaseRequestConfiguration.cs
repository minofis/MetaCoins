using MetaCoins.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class CoinPurchaseRequestConfiguration : IEntityTypeConfiguration<CoinPurchaseRequest>
    {
        public void Configure(EntityTypeBuilder<CoinPurchaseRequest> builder)
        {
            // CoinPurchaseRequest to CoinSellOrder
            builder
                .HasOne(cpr => cpr.CoinSellOrder)
                .WithMany()
                .HasForeignKey(cpr => cpr.CoinSellOrderId);

            // CoinPurchaseRequest to BuyerWallet
            builder
                .HasOne(cpr => cpr.BuyerWallet)
                .WithMany()
                .HasForeignKey(cpr => cpr.BuyerWalletId);

            // CoinPurchaseRequest to CoinPurchaseRequestStatus
            builder
                .HasOne(cpr => cpr.Status)
                .WithMany()
                .HasForeignKey(cpr => cpr.StatusId);
        }
    }
}