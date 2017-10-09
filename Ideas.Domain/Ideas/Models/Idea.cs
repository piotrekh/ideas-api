using System;

namespace Ideas.Domain.Ideas.Models
{
    public class Idea
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
