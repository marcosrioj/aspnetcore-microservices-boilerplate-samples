using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Shared.Authorization
{
    public class SharedAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            //MicroserviceSample Permissions
            context.CreatePermission(PermissionNames.Pages_MicroserviceSample_Product, L("MicroserviceSample.Products"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SharedConsts.LocalizationSourceName);
        }
    }
}
