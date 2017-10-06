using Ideas.DataAccess.Entities.Identity;
using System;

namespace Ideas.DataAccess.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public int AspNetUserId { get; set; }

        public Guid Token { get; set; }

        public int ApiClientId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }


        public virtual ApiClient Client { get; set; }

        public virtual User User { get; set; }
    }
}
