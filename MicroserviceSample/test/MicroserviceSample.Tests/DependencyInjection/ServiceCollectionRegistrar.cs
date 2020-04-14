using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Abp.Dependency;
using MicroserviceSample.EntityFrameworkCore;
using Shared.Registrar;
using Shared.EntityFrameworkCore;

namespace MicroserviceSample.Tests.DependencyInjection
{
    public static class ServiceCollectionRegistrar
    {
        public static void Register(IIocManager iocManager)
        {
            var services = new ServiceCollection();

            IdentityCoreBaseRegistrar.Register(services);

            services.AddEntityFrameworkInMemoryDatabase();

            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);

            var sharedBuilder = new DbContextOptionsBuilder<SharedDbContext>();
            sharedBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);

            var microserviceSampleBuilder = new DbContextOptionsBuilder<MicroserviceSampleDbContext>();
            microserviceSampleBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);

            iocManager.IocContainer.Register(
                Component
                    .For<DbContextOptions<SharedDbContext>>()
                    .Instance(sharedBuilder.Options)
                    .LifestyleSingleton(),
                Component
                    .For<DbContextOptions<MicroserviceSampleDbContext>>()
                    .Instance(microserviceSampleBuilder.Options)
                    .LifestyleSingleton()
            );
        }
    }
}
