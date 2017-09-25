using System;

namespace Ideas.DataAccess.Entities
{
    public class ApiClient
    {
        public int Id { get; set; }

        public Guid ExternalId { get; set; }

        public string Name { get; set; }
    }
}
