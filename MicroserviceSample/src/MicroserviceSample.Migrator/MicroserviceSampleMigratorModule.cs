using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MicroserviceSample.EntityFrameworkCore;
using MicroserviceSample.Migrator.DependencyInjection;
using Shared.Configuration;
using Shared;
using Abp.Domain.Uow;
using Abp.Dependency;

namespace MicroserviceSample.Migrator
{
    [DependsOn(typeof(MicroserviceSampleEntityFrameworkModule))]
    public class MicroserviceSampleMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public MicroserviceSampleMigratorModule(MicroserviceSampleEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(MicroserviceSampleMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.Register<IConnectionStringResolver, DbContextConnectionStringResolver>(DependencyLifeStyle.Transient);
            });

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceSampleMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
