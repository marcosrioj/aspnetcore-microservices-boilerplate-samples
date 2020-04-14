using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Configuration;
using System;

namespace MicroserviceSample.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MicroserviceSampleDbContextFactory : IDesignTimeDbContextFactory<MicroserviceSampleDbContext>
    {
        public MicroserviceSampleDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MicroserviceSampleDbContext>();
            var configuration = AppConfigurations.Get(AppDomain.CurrentDomain.BaseDirectory);

            MicroserviceSampleDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SharedConsts.MicroserviceSampleConnectionStringName));

            return new MicroserviceSampleDbContext(builder.Options);
        }
    }
}
