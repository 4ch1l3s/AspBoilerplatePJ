using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using internPJ3.Configuration;
using internPJ3.Web;

namespace internPJ3.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class internPJ3DbContextFactory : IDesignTimeDbContextFactory<internPJ3DbContext>
    {
        public internPJ3DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<internPJ3DbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            internPJ3DbContextConfigurer.Configure(builder, configuration.GetConnectionString(internPJ3Consts.ConnectionStringName));

            return new internPJ3DbContext(builder.Options);
        }
    }
}
