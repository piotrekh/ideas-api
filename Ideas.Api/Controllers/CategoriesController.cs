using AutoMapper;
using Ideas.Api.Dtos.Categories.Models;
using Ideas.Api.Dtos.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Queries = Ideas.Domain.Categories.Queries;

namespace Ideas.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all categories of ideas
        /// </summary>        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ItemsResult<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
            var queryResult = await _mediator.Send(new Queries.GetCategories());
            var categories = _mapper.Map<ItemsResult<Category>>(queryResult);
            return Ok(categories);
        }
    }
}
