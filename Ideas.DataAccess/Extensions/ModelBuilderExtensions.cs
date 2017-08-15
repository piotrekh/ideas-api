using Ideas.DataAccess.EntitiesConfig;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ideas.DataAccess.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity, TConfig>(this ModelBuilder modelBuilder)
            where TEntity : class
            where TConfig : EntityConfigurationBase<TEntity>
        {
            TConfig config = Activator.CreateInstance<TConfig>();
            modelBuilder.Entity<TEntity>(config.Configure);
        }
    }
}
