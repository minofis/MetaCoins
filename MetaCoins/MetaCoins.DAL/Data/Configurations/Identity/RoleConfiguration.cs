using MetaCoins.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder
                .HasData(
                    new RoleEntity{Id = Guid.Parse("58874eb2-13a9-423e-950d-571b65771274"), Name = "Admin", NormalizedName = "ADMIN"},
                    new RoleEntity{Id = Guid.Parse("b0b3914f-f7ad-4386-80a7-68bf45c776fa"), Name = "Customer", NormalizedName = "CUSTOMER"}
                );
        }
    }
}