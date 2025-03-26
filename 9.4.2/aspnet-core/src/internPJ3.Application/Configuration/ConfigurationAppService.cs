using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using internPJ3.Configuration.Dto;

namespace internPJ3.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : internPJ3AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
