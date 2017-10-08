using Ideas.Domain.Categories.Models;
using MediatR;

namespace Ideas.Domain.Categories.Commands
{
    public class CreateCategory : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
