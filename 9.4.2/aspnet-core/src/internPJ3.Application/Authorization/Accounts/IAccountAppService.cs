using System.Threading.Tasks;
using Abp.Application.Services;
using internPJ3.Authorization.Accounts.Dto;

namespace internPJ3.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
