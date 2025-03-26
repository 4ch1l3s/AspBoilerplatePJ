using System.Threading.Tasks;
using internPJ3.Configuration.Dto;

namespace internPJ3.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
