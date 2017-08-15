using Ideas.DataAccess.Entities.Identity;
using System;
using System.Collections.Generic;

namespace Ideas.DataAccess.Entities
{
    public class Idea
    {
        public int Id { get; set; }

        public int AspNetUserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int IdeaCategoryId { get; set; }

        //----- Navigation properties -----

        public User User { get; set; }

        public IdeaCategory Category { get; set; }

        public ICollection<AssignedIdeaSubcategory> Subcategories { get; set; }
    }
}
