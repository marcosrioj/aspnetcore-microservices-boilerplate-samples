using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace MicroserviceSample.Controllers
{
    public abstract class MicroserviceSampleControllerBase: AbpController
    {
        protected MicroserviceSampleControllerBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
