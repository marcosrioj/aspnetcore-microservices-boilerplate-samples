using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using System;

namespace Shared.Configuration
{
    public static class AppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static AppConfigurations()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets(typeof(AppConfigurations).GetAssembly());
            }

            return builder.Build();
        }

        public static string GetConnectionString(string contextType, IConfigurationRoot configuration = null)
        {
            var connectionString = string.Empty;

            if (configuration == null)
            {
                configuration = Get(AppDomain.CurrentDomain.BaseDirectory);
            }

            switch (contextType)
            {
                case "Shared.EntityFrameworkCore.SharedDbContext":
                    connectionString = configuration.GetConnectionString(SharedConsts.SharedConnectionStringName);
                    break;
                case "MicroserviceSample.EntityFrameworkCore.MicroserviceSampleDbContext":
                    connectionString = configuration.GetConnectionString(SharedConsts.MicroserviceSampleConnectionStringName);
                    break;
                default:
                    connectionString = configuration.GetConnectionString(SharedConsts.SharedConnectionStringName);
                    break;
            }

            return connectionString;
        }
    }
}
