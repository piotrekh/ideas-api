using AutoMapper;
using Ideas.Api.Dtos.Users.Commands;
using DomainCommands = Ideas.Domain.Users.Commands;

namespace Ideas.Api.Dtos.Users.Mapping
{
    public class UsersMappings : Profile
    {
        public UsersMappings()
        {
            CreateMap<ActivateUser, DomainCommands.ActivateUser>();
            CreateMap<CreateUser, DomainCommands.CreateUser>();
        }
    }
}
