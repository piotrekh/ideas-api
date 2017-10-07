using AutoMapper;
using Ideas.Domain.Categories.Mapping;
using System.Reflection;

namespace Ideas.UnitTests.TestDoubles
{
    public static class MapperFactory
    {
        public static IMapper CreateMapper()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(CategoriesMappings).GetTypeInfo().Assembly);
            });

            return mapperConfig.CreateMapper();
        }
    }
}
