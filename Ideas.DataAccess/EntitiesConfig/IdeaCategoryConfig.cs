using Ideas.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal class IdeaCategoryConfig : EntityConfigurationBase<IdeaCategory>
    {
        public override void Configure(EntityTypeBuilder<IdeaCategory> builder)
        {
            builder.ToTable("IdeaCategory", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
