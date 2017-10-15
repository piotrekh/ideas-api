using AutoMapper;
using System.Linq;
using Entities = Ideas.DataAccess.Entities;

namespace Ideas.Domain.Ideas.Mapping
{
    public class IdeasMappings : Profile
    {
        public IdeasMappings()
        {
            CreateMap<Entities.Idea, Models.Idea>();

            CreateMap<Entities.Idea, Models.IdeaDetails>()
                .IncludeBase<Entities.Idea, Models.Idea>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories.Select(x => x.Subcategory)));
        }
    }
}
