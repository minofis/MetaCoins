using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaCoins.DAL.Data.Configurations.Identity
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            /*
            builder
                .HasData(
                    new IdentityUserRole<Guid>{RoleId = Guid.Parse("58874eb2-13a9-423e-950d-571b65771274"), UserId = Guid.Parse("ed442e80-4b55-403a-a160-2fed26a45dc7")}
                );
            */
        }
    }
}