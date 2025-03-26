using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace internPJ3.EntityFrameworkCore
{
    public static class internPJ3DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<internPJ3DbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<internPJ3DbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
