using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ideas.DataAccess.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }        
    }
}
