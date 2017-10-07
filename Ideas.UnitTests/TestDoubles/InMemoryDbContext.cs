using Ideas.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Ideas.UnitTests.TestDoubles
{
    public class InMemoryDbContext : IdeasDbContext
    {
        private int _index = 10000;

        public InMemoryDbContext() : base(CreateNewContextOptions())
        {
        }
        
        private static DbContextOptions<IdeasDbContext> CreateNewContextOptions()
        {
            var builder = new DbContextOptionsBuilder<IdeasDbContext>();
            //set name as new guid so that a new DbContext is created for each test scope
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return builder.Options;
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            //set id (with auto increment) of newly added entities - 
            //EF in memory provider won't do this for us
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                object idValue = idProperty.GetValue(entity);

                if ((idValue is int && (int)idValue == 0)
                    || (idValue is long && (long)idValue == 0))
                {
                    idProperty.SetValue(entity, _index);
                    _index++;
                }
            }

            return base.Add(entity);
        }
    }
}
