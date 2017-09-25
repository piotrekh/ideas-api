using AutoMapper;
using Ideas.Api.Models.Users;
using Commands = Ideas.Domain.Users.Commands;
using UsersModels = Ideas.Domain.Users.Models;

namespace Ideas.Api.Mapping
{
    public class UsersMappings : Profile
    {
        public UsersMappings()
        {
            CreateMap<ActivateUser, Commands.ActivateUser>();
            CreateMap<CreateUser, Commands.CreateUser>();
            CreateMap<TokenRequest, Commands.GetAccessToken>();
            CreateMap<TokenRequest, Commands.RefreshAccessToken>();
            CreateMap<UsersModels.AuthenticationToken, AuthenticationToken>();
        }
    }
}
