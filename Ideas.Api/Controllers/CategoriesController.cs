using Ideas.Domain.Categories.Commands;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;
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
            ItemsResult<Category> categories = await _mediator.Send(new GetCategories());
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
            Category category = await _mediator.Send(command);
            return Ok(category);
        }

        /// <summary>
        /// Returns all subcategories of the given category
        /// </summary>
        [HttpGet("{id}/subcategories")]
        [Authorize]
        [ProducesResponseType(typeof(ItemsResult<Subcategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSubcategories([FromRoute] string id)
        {
            var query = new GetSubcategories() { CategoryId = id };
            ItemsResult<Subcategory> result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Returns ideas from given category
        /// </summary>
        [HttpGet("{id}/ideas")]
        [Authorize]
        [ProducesResponseType(typeof(ItemsResult<Idea>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdeasFromCategory([FromRoute] string id)
        {
            var query = new GetIdeasFromCategory() { CategoryId = id };
            ItemsResult<Idea> result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Returns ideas from given category and subcategory
        /// </summary>
        [HttpGet("{id}/subcategories/{subId}/ideas")]
        [Authorize]
        [ProducesResponseType(typeof(ItemsResult<Idea>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdeasFromSubcategory([FromRoute] string id, [FromRoute] string subId)
        {
            var query = new GetIdeasFromSubcategory() { CategoryId = id, SubcategoryId = subId };
            ItemsResult<Idea> result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
