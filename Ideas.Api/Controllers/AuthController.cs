using AutoMapper;
using Ideas.Domain.Authorization.Commands;
using Ideas.Domain.Authorization.Exceptions;
using Ideas.Domain.Authorization.Models;
using Ideas.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Ideas.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
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
            AuthenticationToken token = null;

            if (request.GrantType?.ToLower() == GrantTypes.Password)
            {
                var command = _mapper.Map<GetAccessToken>(request);
                token = await _mediator.Send(command);
            }
            else if(request.GrantType?.ToLower() == GrantTypes.RefreshToken)
            {
                var command = _mapper.Map<RefreshAccessToken>(request);
                token = await _mediator.Send(command);
            }
            else
            {
                throw new InvalidGrantTypeException();
            }
            
            return Ok(token);
        }
    }
}
