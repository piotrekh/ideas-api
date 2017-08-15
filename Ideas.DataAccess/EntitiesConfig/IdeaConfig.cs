using Ideas.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal class IdeaConfig : EntityConfigurationBase<Idea>
    {
        public override void Configure(EntityTypeBuilder<Idea> builder)
        {
            builder.ToTable("Idea", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.AspNetUserId);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.IdeaCategoryId);
        }
    }
}
