using AutoMapper;
using Ideas.Api.Models.Users;
using Commands = Ideas.Domain.Users.Commands;

namespace Ideas.Api.Mapping
{
    public class UsersMappings : Profile
    {
        public UsersMappings()
        {
            CreateMap<ActivateUser, Commands.ActivateUser>();
            CreateMap<CreateUser, Commands.CreateUser>();
        }
    }
}
