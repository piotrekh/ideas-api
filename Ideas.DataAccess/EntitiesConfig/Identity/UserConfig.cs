using Ideas.DataAccess.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig.Identity
{
    internal class UserConfig : EntityConfigurationBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUser", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                        .ValueGeneratedOnAdd();

            builder.Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken();

            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedEmail)
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedUserName)
                .HasMaxLength(256);

            builder.Property(x => x.UserName)
                .HasMaxLength(256);
        }
    }
}
