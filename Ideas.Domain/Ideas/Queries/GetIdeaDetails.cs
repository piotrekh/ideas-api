using Ideas.Domain.Ideas.Models;
using MediatR;

namespace Ideas.Domain.Ideas.Queries
{
    public class GetIdeaDetails : IRequest<IdeaDetails>
    {
        public string IdeaId { get; set; }
    }
}
