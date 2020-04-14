using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore;
using MicroserviceSample.Entities;

namespace MicroserviceSample.EntityFrameworkCore
{
    public class MicroserviceSampleDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Product> Products { get; set; }

        public MicroserviceSampleDbContext(DbContextOptions<MicroserviceSampleDbContext> options)
            : base(options)
        {

        }
    }
}
