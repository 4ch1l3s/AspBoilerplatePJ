using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using internPJ3.Configuration;

namespace internPJ3.Web.Host.Startup
{
    [DependsOn(
       typeof(internPJ3WebCoreModule))]
    public class internPJ3WebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public internPJ3WebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(internPJ3WebHostModule).GetAssembly());
        }
    }
}
