using System;
using System.IO;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Shared.Configuration;

namespace Shared
{
    public class DbContextConnectionStringResolver : DefaultConnectionStringResolver
    {
        public DbContextConnectionStringResolver(
            IAbpStartupConfiguration configuration)
            : base(configuration)
        {
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var connectionString = AppConfigurations.GetConnectionString(args["DbContextType"]?.ToString());
            return connectionString;
        }
    }
}