using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore;

namespace MicroserviceBaseProject.EntityFrameworkCore
{
    public class MicroserviceBaseProjectDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */

        public MicroserviceBaseProjectDbContext(DbContextOptions<MicroserviceBaseProjectDbContext> options)
            : base(options)
        {

        }
    }
}
