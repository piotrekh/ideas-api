using AutoMapper;
using Ideas.UnitTests.TestDoubles;
using Xunit;

namespace Ideas.UnitTests
{
    public class MappingsTest
    {
        [Fact]
        public void DomainMappings_Should_BeValid()
        {
            IMapper mapper = MapperFactory.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
