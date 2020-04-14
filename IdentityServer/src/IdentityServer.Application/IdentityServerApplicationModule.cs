using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Shared.Authorization;

namespace IdentityServer
{
    [DependsOn(
        typeof(IdentityServerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class IdentityServerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SharedAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(IdentityServerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
