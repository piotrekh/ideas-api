using AutoMapper;
using Entities = Ideas.DataAccess.Entities;

namespace Ideas.Domain.Categories.Mapping
{
    public class CategoriesMappings : Profile
    {
        public CategoriesMappings()
        {
            CreateMap<Entities.IdeaCategory, Models.Category>();
        }
    }
}
