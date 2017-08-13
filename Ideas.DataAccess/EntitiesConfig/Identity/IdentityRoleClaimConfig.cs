using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityRoleClaimConfig : EntityConfigurationBase<IdentityRoleClaim<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("AspNetRoleClaim", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                        .ValueGeneratedOnAdd();

            builder.Property(x => x.RoleId)
                .HasColumnName("AspNetRoleId");
        }
    }
}
