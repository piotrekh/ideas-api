using Ideas.Domain.Categories.Commands;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Ideas.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all categories of ideas
        /// </summary>        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ItemsResult<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategories());
            return Ok(categories);
        }

        /// <summary>
        /// Creates category and returns its data
        /// </summary>
        [HttpPost]
        [Authorize(Roles = nameof(RoleName.Admin))]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }
    }
}
