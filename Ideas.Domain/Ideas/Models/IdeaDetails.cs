using Ideas.Domain.Users.Models;
using System.Collections.Generic;

namespace Ideas.Domain.Ideas.Models
{
    public class IdeaDetails : Idea
    {
        public User Author { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string Category { get; set; }

        public List<string> Subcategories { get; set; }
    }
}
