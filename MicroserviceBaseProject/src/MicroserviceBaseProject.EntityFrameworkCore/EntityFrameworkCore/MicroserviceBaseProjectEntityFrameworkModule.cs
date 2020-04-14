using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using MicroserviceBaseProject.EntityFrameworkCore.Seed;
using Shared.Configuration;
using Shared.EntityFrameworkCore;

namespace MicroserviceBaseProject.EntityFrameworkCore
{
    [DependsOn(
        typeof(MicroserviceBaseProjectCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(SharedEntityFrameworkModule)
        )]
    public class MicroserviceBaseProjectEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<MicroserviceBaseProjectDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        MicroserviceBaseProjectDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        var connectionString = AppConfigurations.GetConnectionString(options.DbContextOptions.Options.ContextType.ToString());
                        MicroserviceBaseProjectDbContextConfigurer.Configure(options.DbContextOptions, connectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceBaseProjectEntityFrameworkModule).GetAssembly());
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
