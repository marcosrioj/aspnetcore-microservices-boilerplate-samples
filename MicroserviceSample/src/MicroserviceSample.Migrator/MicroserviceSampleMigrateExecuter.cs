using System;
using System.Collections.Generic;
using System.Data.Common;
using Abp.Data;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using MicroserviceSample.EntityFrameworkCore;
using MicroserviceSample.EntityFrameworkCore.Seed;
using MicroserviceSample.Migrator;
using Shared.MultiTenancy;

namespace Shared.Migrator
{
    public class MicroserviceSampleMigrateExecuter : ITransientDependency
    {
        private readonly Log _log;
        private readonly AbpZeroDbMigrator _migrator;
        private readonly IDbPerTenantConnectionStringResolver _connectionStringResolver;

        public MicroserviceSampleMigrateExecuter(
            AbpZeroDbMigrator migrator,
            Log log,
            IDbPerTenantConnectionStringResolver connectionStringResolver)
        {
            _log = log;
            _migrator = migrator;
            _connectionStringResolver = connectionStringResolver;
        }

        public bool Run(bool skipConnVerification)
        {
            var hostConnStr = CensorConnectionString(_connectionStringResolver.GetNameOrConnectionString(new ConnectionStringResolveArgs(MultiTenancySides.Host)));
            if (hostConnStr.IsNullOrWhiteSpace())
            {
                _log.Write("Configuration file should contain a connection string named 'Default'");
                return false;
            }

            _log.Write("Host database: " + ConnectionStringHelper.GetConnectionString(hostConnStr));
            if (!skipConnVerification)
            {
                _log.Write("Continue to migration for this host database and all tenants..? (Y/N): ");
                var command = Console.ReadLine();
                if (!command.IsIn("Y", "y"))
                {
                    _log.Write("Migration canceled.");
                    return false;
                }
            }

            _log.Write("HOST database migration started...");

            try
            {
                _migrator.CreateOrMigrateForHost(SeedHelper.SeedHostDb);
            }
            catch (Exception ex)
            {
                _log.Write("An error occured during migration of host database:");
                _log.Write(ex.ToString());
                _log.Write("Canceled migrations.");
                return false;
            }

            _log.Write("HOST database migration completed.");
            _log.Write("--------------------------------------------------------");

            return true;
        }

        private static string CensorConnectionString(string connectionString)
        {
            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            var keysToMask = new[] { "password", "pwd", "user id", "uid" };

            foreach (var key in keysToMask)
            {
                if (builder.ContainsKey(key))
                {
                    builder[key] = "*****";
                }
            }

            return builder.ToString();
        }
    }
}
