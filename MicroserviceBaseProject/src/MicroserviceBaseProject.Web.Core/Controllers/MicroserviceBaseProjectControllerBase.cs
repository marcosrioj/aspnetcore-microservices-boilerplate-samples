using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace MicroserviceBaseProject.Controllers
{
    public abstract class MicroserviceBaseProjectControllerBase: AbpController
    {
        protected MicroserviceBaseProjectControllerBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
