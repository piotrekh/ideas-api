using System;

namespace Ideas.DataAccess.Entities
{
    public class IdeaSubcategory
    {
        public int Id { get; set; }

        public int IdeaCategoryId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public IdeaCategory Category { get; set; }
    }
}
