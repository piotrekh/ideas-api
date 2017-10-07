using AutoMapper;
using Ideas.Domain.Authorization.Commands;
using Ideas.Domain.Authorization.Models;

namespace Ideas.Domain.Authorization.Mapping
{
    public class AuthorizationMappings : Profile
    {
        public AuthorizationMappings()
        {
            CreateMap<TokenRequest, GetAccessToken>();
            CreateMap<TokenRequest, RefreshAccessToken>();
        }
    }
}
