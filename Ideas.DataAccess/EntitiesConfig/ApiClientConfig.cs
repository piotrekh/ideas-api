using Ideas.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal class ApiClientConfig : EntityConfigurationBase<ApiClient>
    {
        public override void Configure(EntityTypeBuilder<ApiClient> builder)
        {
            builder.ToTable("ApiClient", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
