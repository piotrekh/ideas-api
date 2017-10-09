using Ideas.Domain.Common.Models;
using Ideas.Domain.Ideas.Models;
using MediatR;

namespace Ideas.Domain.Ideas.Queries
{
    public class GetIdeasFromCategory : IRequest<ItemsResult<Idea>>
    {
        public string CategoryId { get; set; }
    }
}
