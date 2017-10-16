using Ideas.Domain.Ideas.Models;
using MediatR;
using System.Collections.Generic;

namespace Ideas.Domain.Ideas.Commands
{
    public class CreateIdea : IRequest<IdeaDetails>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        /// <summary>
        /// Names of subcategories. Maximum 5.
        /// </summary>
        public List<string> Subcategories { get; set; }
    }
}
