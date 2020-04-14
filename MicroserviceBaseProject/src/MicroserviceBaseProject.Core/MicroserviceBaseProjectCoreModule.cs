using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;
using Shared;

namespace MicroserviceBaseProject
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(SharedCoreModule))]
    public class MicroserviceBaseProjectCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceBaseProjectCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}
