using Ideas.DataAccess.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig.Identity
{
    internal class RoleConfig : EntityConfigurationBase<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(name: "AspNetRole", schema: "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken();

            builder.Property(x => x.Name)
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedName)
                .HasMaxLength(256);

            builder.HasMany(x => x.Claims)
                   .WithOne()
                   .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.Users)
                   .WithOne()
                   .HasForeignKey(x => x.UserId);
        }
    }
}
