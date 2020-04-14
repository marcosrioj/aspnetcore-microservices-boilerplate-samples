using System;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using MicroserviceSample.EntityFrameworkCore;
using MicroserviceSample.Tests.DependencyInjection;
using Shared.EntityFrameworkCore;

namespace MicroserviceSample.Tests
{
    [DependsOn(
        typeof(MicroserviceSampleApplicationModule),
        typeof(SharedEntityFrameworkModule),
        typeof(MicroserviceSampleEntityFrameworkModule),
        typeof(AbpTestBaseModule)
        )]
    public class MicroserviceSampleTestModule : AbpModule
    {
        public MicroserviceSampleTestModule(
            MicroserviceSampleEntityFrameworkModule microserviceSampleEntityFrameworkModule,
            SharedEntityFrameworkModule sharedEntityFrameworkModule)
        {
            microserviceSampleEntityFrameworkModule.SkipDbContextRegistration = true;
            microserviceSampleEntityFrameworkModule.SkipDbSeed = true;

            sharedEntityFrameworkModule.SkipDbContextRegistration = true;
            sharedEntityFrameworkModule.SkipDbSeed = true;
        }

        public override void PreInitialize()
        {
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
            Configuration.UnitOfWork.IsTransactional = false;

            // Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<AbpZeroDbMigrator<SharedDbContext>>();
            RegisterFakeService<AbpZeroDbMigrator<MicroserviceSampleDbContext>>();

            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            ServiceCollectionRegistrar.Register(IocManager);
        }

        private void RegisterFakeService<TService>() where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }
    }
}
