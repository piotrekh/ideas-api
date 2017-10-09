using AutoMapper;
using Entities = Ideas.DataAccess.Entities;

namespace Ideas.Domain.Ideas.Mapping
{
    public class IdeasMappings : Profile
    {
        public IdeasMappings()
        {
            CreateMap<Entities.Idea, Models.Idea>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
