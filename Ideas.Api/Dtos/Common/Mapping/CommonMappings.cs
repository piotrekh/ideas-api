using AutoMapper;
using DomainModels = Ideas.Domain.Common.Models;

namespace Ideas.Api.Dtos.Common.Mapping
{
    public class CommonMappings : Profile
    {
        public CommonMappings()
        {
            CreateMap(typeof(DomainModels.ItemsResult<>), typeof(ItemsResult<>));
        }
    }
}
