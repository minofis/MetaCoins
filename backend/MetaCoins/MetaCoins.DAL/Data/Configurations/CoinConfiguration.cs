using MetaCoins.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations
{
    public class CoinConfiguration : IEntityTypeConfiguration<Coin>
    {
        public void Configure(EntityTypeBuilder<Coin> builder)
        {
            // Coin to CoinVotingSessions
            builder 
                .HasMany(c => c.CoinVotingSessions)
                .WithOne(cvs => cvs.Coin)
                .HasForeignKey(cvs => cvs.CoinId);

            // Coin to Wallet
            builder
                .HasOne(c => c.Wallet)
                .WithMany(w => w.Coins)
                .HasForeignKey(c => c.WalletId);

            // Coin to OwnershipRecords
            builder
                .HasMany(c => c.OwnershipRecords)
                .WithOne(o => o.Coin)
                .HasForeignKey(o => o.CoinId);

            // Coin to Likes
            builder 
                .HasMany(c => c.Likes)
                .WithOne(l => l.Coin)
                .HasForeignKey(l => l.CoinId);
        }
    }
}