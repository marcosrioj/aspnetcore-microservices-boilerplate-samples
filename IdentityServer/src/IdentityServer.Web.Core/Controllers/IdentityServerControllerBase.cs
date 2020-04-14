using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace IdentityServer.Controllers
{
    public abstract class IdentityServerControllerBase: AbpController
    {
        protected IdentityServerControllerBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
