using Ideas.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal class RefreshTokenConfig : EntityConfigurationBase<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ApiClientId);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.AspNetUserId);
        }
    }
}
