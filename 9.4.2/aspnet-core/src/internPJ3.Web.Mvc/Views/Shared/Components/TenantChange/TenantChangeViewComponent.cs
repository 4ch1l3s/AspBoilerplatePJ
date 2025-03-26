using System.Threading.Tasks;
using Abp.ObjectMapping;
using internPJ3.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace internPJ3.Web.Views.Shared.Components.TenantChange
{
    public class TenantChangeViewComponent : internPJ3ViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IObjectMapper _objectMapper;

        public TenantChangeViewComponent(ISessionAppService sessionAppService, IObjectMapper objectMapper)
        {
            _sessionAppService = sessionAppService;
            _objectMapper = objectMapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            var model = _objectMapper.Map<TenantChangeViewModel>(loginInfo);
            return View(model);
        }
    }
}
