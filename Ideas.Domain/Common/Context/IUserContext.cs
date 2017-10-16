using Ideas.Domain.Common.Enums;

namespace Ideas.Domain.Common.Context
{
    public interface IUserContext
    {
        string Email { get; }

        int? Id { get; }

        bool IsInRole(RoleName role);
    }
}