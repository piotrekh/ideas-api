using Ideas.Domain.Categories.Models;
using Ideas.Domain.Users.Models;
using System.Collections.Generic;

namespace Ideas.Domain.Ideas.Models
{
    public class IdeaDetails : Idea
    {
        public User Author { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public List<Subcategory> Subcategories { get; set; }
    }
}
