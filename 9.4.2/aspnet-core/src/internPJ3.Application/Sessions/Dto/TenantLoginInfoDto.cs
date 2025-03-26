using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using internPJ3.MultiTenancy;

namespace internPJ3.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
