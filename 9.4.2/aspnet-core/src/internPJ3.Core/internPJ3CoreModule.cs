using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using internPJ3.Authorization.Roles;
using internPJ3.Authorization.Users;
using internPJ3.Configuration;
using internPJ3.Localization;
using internPJ3.MultiTenancy;
using internPJ3.Timing;

namespace internPJ3
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class internPJ3CoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            internPJ3LocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = internPJ3Consts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            Configuration.Localization.Languages.Add(new LanguageInfo("vi", "Tiếng Việt", "famfamfam-flags vn"));

            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = internPJ3Consts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = internPJ3Consts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(internPJ3CoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
