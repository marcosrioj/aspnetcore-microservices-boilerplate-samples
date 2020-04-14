using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Shared.Authorization.Roles;
using Shared.Authorization.Users;
using Shared.MultiTenancy;

namespace Shared.EntityFrameworkCore
{
    public class SharedDbContext : AbpZeroDbContext<Tenant, Role, User, SharedDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public SharedDbContext(DbContextOptions<SharedDbContext> options)
            : base(options)
        {
        }
    }
}
