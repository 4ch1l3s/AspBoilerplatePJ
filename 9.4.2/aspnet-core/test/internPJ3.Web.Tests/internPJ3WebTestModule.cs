using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using internPJ3.EntityFrameworkCore;
using internPJ3.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace internPJ3.Web.Tests
{
    [DependsOn(
        typeof(internPJ3WebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class internPJ3WebTestModule : AbpModule
    {
        public internPJ3WebTestModule(internPJ3EntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(internPJ3WebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(internPJ3WebMvcModule).Assembly);
        }
    }
}