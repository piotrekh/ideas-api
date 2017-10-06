using AutoMapper;
using Ideas.Api.Dtos.Categories.Models;
using DomainModels = Ideas.Domain.Categories.Models;


namespace Ideas.Api.Dtos.Categories.Mapping
{
    public class CategoriesMappings : Profile
    {
        public CategoriesMappings()
        {
            CreateMap<DomainModels.Category, Category>();
        }
    }
}
