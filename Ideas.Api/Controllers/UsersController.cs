using Ideas.Domain.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Ideas.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new user, sending them a confirmation link
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Sets user password and activates account
        /// </summary>
        [HttpPut("activation")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUser command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
