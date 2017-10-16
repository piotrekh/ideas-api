﻿using Ideas.Domain.Ideas.Commands;
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
    public class IdeasController : Controller
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
            var result = await _mediator.Send(query);
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
    }
}
