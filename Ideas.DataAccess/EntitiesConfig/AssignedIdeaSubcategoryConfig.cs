using Ideas.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal class AssignedIdeaSubcategoryConfig : EntityConfigurationBase<AssignedIdeaSubcategory>
    {
        public override void Configure(EntityTypeBuilder<AssignedIdeaSubcategory> builder)
        {
            builder.ToTable("AssignedIdeaSubcategory", "dbo");

            builder.HasKey(x => new { x.IdeaId, x.IdeaSubcategoryId });

            builder.HasOne(x => x.Idea)
                .WithMany(y => y.Subcategories)
                .HasForeignKey(x => x.IdeaId);

            builder.HasOne(x => x.Subcategory)
                .WithMany()
                .HasForeignKey(x => x.IdeaSubcategoryId);
        }
    }
}
