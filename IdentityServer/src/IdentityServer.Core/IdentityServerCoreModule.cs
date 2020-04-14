using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Shared;
using Shared.Authorization.Roles;
using Shared.Authorization.Users;
using Shared.Configuration;
using Shared.Localization;
using Shared.MultiTenancy;
using Shared.Timing;

namespace IdentityServer
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(SharedCoreModule))]
    public class IdentityServerCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IdentityServerCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}
