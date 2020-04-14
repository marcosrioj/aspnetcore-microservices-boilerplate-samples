using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Authorization.Roles;
using Shared.Authorization.Users;
using Shared.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Registrar
{
    public static class IdentityCoreBaseRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>();
        }
    }
}
