using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBaseProject.EntityFrameworkCore
{
    public static class MicroserviceBaseProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MicroserviceBaseProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MicroserviceBaseProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
