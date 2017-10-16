using MediatR;

namespace Ideas.Domain.Ideas.Commands
{
    public class DeleteIdea : IRequest
    {
        public string IdeaId { get; set; }
    }
}
