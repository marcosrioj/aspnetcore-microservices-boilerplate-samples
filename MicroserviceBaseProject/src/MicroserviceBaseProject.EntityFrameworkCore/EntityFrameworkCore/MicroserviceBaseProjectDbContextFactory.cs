using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Configuration;
using System;

namespace MicroserviceBaseProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MicroserviceBaseProjectDbContextFactory : IDesignTimeDbContextFactory<MicroserviceBaseProjectDbContext>
    {
        public MicroserviceBaseProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MicroserviceBaseProjectDbContext>();
            var configuration = AppConfigurations.Get(AppDomain.CurrentDomain.BaseDirectory);

            MicroserviceBaseProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SharedConsts.MicroserviceBaseProjectConnectionStringName));

            return new MicroserviceBaseProjectDbContext(builder.Options);
        }
    }
}
