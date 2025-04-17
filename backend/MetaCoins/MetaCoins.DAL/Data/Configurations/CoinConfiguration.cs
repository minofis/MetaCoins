using MetaCoins.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class CoinConfiguration : IEntityTypeConfiguration<Coin>
    {
        public void Configure(EntityTypeBuilder<Coin> builder)
        {
            builder
                .HasOne(c => c.Wallet)
                .WithMany(w => w.Coins)
                .HasForeignKey(c => c.WalletId);

            builder
                .HasMany(c => c.OwnershipRecords)
                .WithOne(o => o.Coin)
                .HasForeignKey(o => o.CoinId);

            builder 
                .HasMany(c => c.Likes)
                .WithOne(l => l.Coin)
                .HasForeignKey(l => l.CoinId);
        }
    }
}