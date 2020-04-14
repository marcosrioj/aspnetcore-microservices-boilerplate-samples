using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared.Configuration;
using System;

namespace Shared.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SharedDbContextFactory : IDesignTimeDbContextFactory<SharedDbContext>
    {
        public SharedDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SharedDbContext>();
            var configuration = AppConfigurations.Get(AppDomain.CurrentDomain.BaseDirectory);

            SharedDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SharedConsts.SharedConnectionStringName));

            return new SharedDbContext(builder.Options);
        }
    }
}
