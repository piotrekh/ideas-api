using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ideas.DataAccess.EntitiesConfig
{
    internal abstract class EntityConfigurationBase<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
