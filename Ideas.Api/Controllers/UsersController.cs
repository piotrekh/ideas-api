using AutoMapper;
using Ideas.Api.Dtos.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Commands = Ideas.Domain.Users.Commands;

namespace Ideas.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new user, sending them a confirmation link
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
        {
            var command = _mapper.Map<Commands.CreateUser>(user);

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Sets user password and activates account
        /// </summary>
        [HttpPut("activation")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUser activation)
        {
            var command = _mapper.Map<Commands.ActivateUser>(activation);

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
