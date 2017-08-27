using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityUserTokenConfig : EntityConfigurationBase<IdentityUserToken<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
        {
            builder.ToTable("AspNetUserToken", "dbo");

            builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            builder.Property(x => x.UserId)
                .HasColumnName("AspNetUserId");
        }
    }
}
