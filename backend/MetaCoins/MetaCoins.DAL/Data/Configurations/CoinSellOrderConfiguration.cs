using MetaCoins.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class CoinSellOrderConfiguration : IEntityTypeConfiguration<CoinSellOrder>
    {
        public void Configure(EntityTypeBuilder<CoinSellOrder> builder)
        {
            // CoinSellOrder to Coin
            builder
                .HasOne(cso => cso.Coin)
                .WithMany()
                .HasForeignKey(cso => cso.CoinId);

            // CoinSellOrder to SellerWallet
            builder
                .HasOne(cso => cso.SellerWallet)
                .WithMany()
                .HasForeignKey(cso => cso.SellerWalletId);

            // CoinSellOrder to CoinSellOrderStatus
            builder
                .HasOne(cso => cso.Status)
                .WithMany()
                .HasForeignKey(cso => cso.StatusId);
        }
    }
}