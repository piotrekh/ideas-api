using Ideas.Domain.Common.Models;
using Ideas.Domain.Ideas.Commands;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Ideas.Api.Controllers
{
    [Route("/api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class IdeasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdeasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns idea details
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IdeaDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdeaDetails([FromRoute] string id)
        {
            var query = new GetIdeaDetails() { IdeaId = id };
            IdeaDetails result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Returns predefined number of newest ideas
        /// </summary>
        [HttpGet("newest")]
        [ProducesResponseType(typeof(ItemsResult<Idea>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetNewestIdeas()
        {
            var query = new GetNewestIdeas();
            ItemsResult<Idea> result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Deletes idea
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteIdea([FromRoute] string id)
        {
            var command = new DeleteIdea() { IdeaId = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Creates a new idea
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(IdeaDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateIdea([FromBody] CreateIdea command)
        {
            IdeaDetails result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
