using System.Threading.Tasks;
using Abp.Application.Services;
using internPJ3.Sessions.Dto;

namespace internPJ3.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
