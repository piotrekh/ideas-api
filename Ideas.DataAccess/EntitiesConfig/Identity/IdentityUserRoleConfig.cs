using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityUserRoleConfig : EntityConfigurationBase<IdentityUserRole<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.ToTable("AspNetUserRole", "dbo");

            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.Property(x => x.UserId)
                .HasColumnName("AspNetUserId");

            builder.Property(x => x.RoleId)
                .HasColumnName("AspNetRoleId");
        }
    }
}
