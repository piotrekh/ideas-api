using Ideas.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal class IdeaSubcategoryConfig : EntityConfigurationBase<IdeaSubcategory>
    {
        public override void Configure(EntityTypeBuilder<IdeaSubcategory> builder)
        {
            builder.ToTable("IdeaSubcategory", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.IdeaCategoryId);
        }
    }
}
