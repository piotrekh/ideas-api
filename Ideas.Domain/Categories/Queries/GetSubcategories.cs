using Ideas.Domain.Categories.Models;
using Ideas.Domain.Common.Models;
using MediatR;

namespace Ideas.Domain.Categories.Queries
{
    public class GetSubcategories : IRequest<ItemsResult<Subcategory>>
    {
        public string CategoryId { get; set; }
    }
}
