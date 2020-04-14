using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Shared.Authorization;

namespace MicroserviceSample
{
    [DependsOn(
        typeof(MicroserviceSampleCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MicroserviceSampleApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SharedAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MicroserviceSampleApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
