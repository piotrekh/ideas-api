using Ideas.DataAccess;
using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Settings;
using Ideas.Domain.Users.Commands;
using Ideas.Domain.Users.Models;
using Ideas.Domain.Users.Services;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.CommandHandlers
{
    public class GenerateAuthenticationTokensHandler : IAsyncRequestHandler<GenerateAuthenticationTokens, AuthenticationToken>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsersService _usersService;
        private readonly AuthSettings _authSettings;

        public GenerateAuthenticationTokensHandler(IUnitOfWork uow,
            IUsersService usersService,
            IOptions<AuthSettings> authSettings)
        {
            _uow = uow;
            _usersService = usersService;
            _authSettings = authSettings.Value;
        }

        public async Task<AuthenticationToken> Handle(GenerateAuthenticationTokens message)
        {
            //retrieve claims
            IEnumerable<Claim> claims = await _usersService.GetUserSimpleClaims(message.User);
            Dictionary<string, object> complexClaims = await _usersService.GetUserComplexClaims(message.User);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_authSettings.TokenExpiration),
                signingCredentials: creds
            );

            //insert complex claims            
            foreach (var claim in complexClaims)
                token.Payload.Add(claim.Key, claim.Value);

            //generate a new refresh token
            Guid refreshToken = GenerateRefreshToken(message.User, message.Client.Id);

            return new AuthenticationToken()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = _authSettings.TokenExpiration * 60,
                TokenType = "Bearer",
                RefreshToken = refreshToken
            };
        }

        private Guid GenerateRefreshToken(User user, int clientId)
        {
            var tokenEntity = new RefreshToken()
            {
                ApiClientId = clientId,
                AspNetUserId = user.Id,
                IssueDate = DateTime.UtcNow,
                Token = Guid.NewGuid(),
                ExpirationDate = DateTime.UtcNow.AddMinutes(_authSettings.RefreshTokenExpiration)
            };
            _uow.RefreshTokens.Add(tokenEntity);
            _uow.SaveChanges();

            return tokenEntity.Token;
        }
    }
}
