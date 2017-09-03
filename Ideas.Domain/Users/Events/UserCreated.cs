using Ideas.DataAccess.Entities.Identity;
using MediatR;

namespace Ideas.Domain.Users.Events
{
    public class UserCreated : INotification
    {
        public User User { get; set; }
    }
}
