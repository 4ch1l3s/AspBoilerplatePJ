using Abp.Application.Services;
using internPJ3.MultiTenancy.Dto;

namespace internPJ3.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

