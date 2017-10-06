using AutoMapper;
using Ideas.Api.Dtos.Users.Commands;
using Ideas.Api.Dtos.Users.Models;
using DomainCommands = Ideas.Domain.Users.Commands;
using DomainModels = Ideas.Domain.Users.Models;

namespace Ideas.Api.Dtos.Users.Mapping
{
    public class UsersMappings : Profile
    {
        public UsersMappings()
        {
            CreateMap<ActivateUser, DomainCommands.ActivateUser>();
            CreateMap<CreateUser, DomainCommands.CreateUser>();
            CreateMap<TokenRequest, DomainCommands.GetAccessToken>();
            CreateMap<TokenRequest, DomainCommands.RefreshAccessToken>();
            CreateMap<DomainModels.AuthenticationToken, AuthenticationToken>();
        }
    }
}
