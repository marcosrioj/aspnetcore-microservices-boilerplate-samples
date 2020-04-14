using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Shared;

namespace MicroserviceSample
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MicroserviceSampleAppServiceBase : ApplicationService
    {
        protected MicroserviceSampleAppServiceBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }
    }
}
