using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Shared.Authorization;

namespace MicroserviceBaseProject
{
    [DependsOn(
        typeof(MicroserviceBaseProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MicroserviceBaseProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SharedAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MicroserviceBaseProjectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
