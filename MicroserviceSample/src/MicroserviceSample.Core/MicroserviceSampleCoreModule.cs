using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;
using Shared;

namespace MicroserviceSample
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(SharedCoreModule))]
    public class MicroserviceSampleCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceSampleCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}
