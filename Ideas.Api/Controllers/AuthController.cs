using AutoMapper;
using Ideas.Api.Dtos.Users.Models;
using Ideas.Api.Exceptions;
using Ideas.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Commands = Ideas.Domain.Users.Commands;
using UsersModels = Ideas.Domain.Users.Models;

namespace Ideas.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Allows to generate or refresh access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [ProducesResponseType(typeof(AuthenticationToken), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccessToken([FromBody] TokenRequest request)
        {
            UsersModels.AuthenticationToken commandResult = null;

            if (request.GrantType?.ToLower() == GrantTypes.Password)
            {
                var command = _mapper.Map<Commands.GetAccessToken>(request);
                commandResult = await _mediator.Send(command);
            }
            else if(request.GrantType?.ToLower() == GrantTypes.RefreshToken)
            {
                var command = _mapper.Map<Commands.RefreshAccessToken>(request);
                commandResult = await _mediator.Send(command);
            }
            else
            {
                throw new InvalidGrantTypeException();
            }

            var token = _mapper.Map<AuthenticationToken>(commandResult);
            return Ok(token);
        }
    }
}
