using Abp.AspNetCore.Mvc.ViewComponents;

namespace internPJ3.Web.Views
{
    public abstract class internPJ3ViewComponent : AbpViewComponent
    {
        protected internPJ3ViewComponent()
        {
            LocalizationSourceName = internPJ3Consts.LocalizationSourceName;
        }
    }
}
