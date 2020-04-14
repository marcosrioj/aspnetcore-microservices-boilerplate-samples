using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Shared;

namespace MicroserviceBaseProject
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MicroserviceBaseProjectAppServiceBase : ApplicationService
    {
        protected MicroserviceBaseProjectAppServiceBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }
    }
}
