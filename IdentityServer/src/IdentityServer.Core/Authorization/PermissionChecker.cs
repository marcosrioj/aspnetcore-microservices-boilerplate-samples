using Abp.Authorization;
using IdentityServer.Authorization.Users;
using Shared.Authorization.Roles;
using Shared.Authorization.Users;

namespace IdentityServer.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
