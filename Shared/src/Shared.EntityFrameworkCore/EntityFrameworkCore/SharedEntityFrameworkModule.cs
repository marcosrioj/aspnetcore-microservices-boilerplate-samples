using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Configuration;
using Shared.EntityFrameworkCore.Seed;
using System;

namespace Shared.EntityFrameworkCore
{
    [DependsOn(
        typeof(SharedCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class SharedEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<SharedDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SharedDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        var connectionString = AppConfigurations.GetConnectionString(options.DbContextOptions.Options.ContextType.ToString());
                        SharedDbContextConfigurer.Configure(options.DbContextOptions, connectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharedEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
