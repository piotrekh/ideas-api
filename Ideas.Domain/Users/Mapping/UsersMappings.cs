using AutoMapper;
using Entities = Ideas.DataAccess.Entities;

namespace Ideas.Domain.Users.Mapping
{
    public class UsersMappings : Profile
    {
        public UsersMappings()
        {
            CreateMap<Entities.Identity.User, Models.User>();
        }
    }
}
