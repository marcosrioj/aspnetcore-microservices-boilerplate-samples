using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MicroserviceBaseProject.EntityFrameworkCore;
using MicroserviceBaseProject.Migrator.DependencyInjection;
using Shared.Configuration;
using Shared;
using Abp.Domain.Uow;
using Abp.Dependency;

namespace MicroserviceBaseProject.Migrator
{
    [DependsOn(typeof(MicroserviceBaseProjectEntityFrameworkModule))]
    public class MicroserviceBaseProjectMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public MicroserviceBaseProjectMigratorModule(MicroserviceBaseProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(MicroserviceBaseProjectMigratorModule).GetAssembly().GetDirectoryPathOrNull()
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
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceBaseProjectMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
