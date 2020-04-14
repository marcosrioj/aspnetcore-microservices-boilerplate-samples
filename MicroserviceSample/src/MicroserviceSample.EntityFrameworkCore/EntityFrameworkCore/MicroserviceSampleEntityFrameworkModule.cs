using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using MicroserviceSample.EntityFrameworkCore.Seed;
using Shared.Configuration;
using Shared.EntityFrameworkCore;

namespace MicroserviceSample.EntityFrameworkCore
{
    [DependsOn(
        typeof(MicroserviceSampleCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(SharedEntityFrameworkModule)
        )]
    public class MicroserviceSampleEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<MicroserviceSampleDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        MicroserviceSampleDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        var connectionString = AppConfigurations.GetConnectionString(options.DbContextOptions.Options.ContextType.ToString());
                        MicroserviceSampleDbContextConfigurer.Configure(options.DbContextOptions, connectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceSampleEntityFrameworkModule).GetAssembly());
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
