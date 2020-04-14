using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSample.EntityFrameworkCore
{
    public static class MicroserviceSampleDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MicroserviceSampleDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MicroserviceSampleDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
