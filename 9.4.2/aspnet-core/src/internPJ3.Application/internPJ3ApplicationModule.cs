using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using internPJ3.Authorization;

namespace internPJ3
{
    [DependsOn(
        typeof(internPJ3CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class internPJ3ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<internPJ3AuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(internPJ3ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
