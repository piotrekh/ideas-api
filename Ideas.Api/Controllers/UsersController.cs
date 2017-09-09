using Ideas.Api.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Commands = Ideas.Domain.Users.Commands;

namespace Ideas.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
        {
            Commands.CreateUser command = new Commands.CreateUser()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
