using System;
using System.IO;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Shared.Configuration;

namespace Shared
{
    public class DbContextConnectionStringWebHostResolver : DefaultConnectionStringResolver
    {
        private readonly IWebHostEnvironment _env;

        public DbContextConnectionStringWebHostResolver(
            IAbpStartupConfiguration configuration,
            IWebHostEnvironment env)
            : base(configuration)
        {
            _env = env;
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var configuration = AppConfigurations.Get(_env.ContentRootPath, _env.EnvironmentName);
            var connectionString = AppConfigurations.GetConnectionString(args["DbContextType"]?.ToString(), configuration);
            return connectionString;
        }
    }
}