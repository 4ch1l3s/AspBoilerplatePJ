using Abp.Authorization;
using internPJ3.Authorization.Roles;
using internPJ3.Authorization.Users;

namespace internPJ3.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
