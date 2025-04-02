using MetaCoins.Core.Entities;
using MetaCoins.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            /*
            builder
                .HasData(
                    new UserEntity
                    {
                        Id = Guid.Parse("ed442e80-4b55-403a-a160-2fed26a45dc7"),
                        UserName = "admin",
                        Email = "admin@mail.com",
                        PasswordHash = "AQAAAAIAAYagAAAAEGK35bXYasW9FUAyaVxmN7XxMXukoxqbUZR0SeRjXKyT/6pmQ+sa32ptd0KLMmO66Q=="
                    }
                );
                */
            builder 
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<UserEntity>(u => u.WalletId);

            builder 
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserEntity>(u => u.ProfileId);

            builder 
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            builder 
                .HasMany(u => u.Votes)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId);
        }
    }
}