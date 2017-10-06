using Ideas.Domain.Categories.Models;
using Ideas.Domain.Common.Models;
using MediatR;

namespace Ideas.Domain.Categories.Queries
{
    public class GetCategories : IRequest<ItemsResult<Category>>
    {
    }
}
