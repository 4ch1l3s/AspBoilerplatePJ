using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace internPJ3.Web.Views
{
    public abstract class internPJ3RazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected internPJ3RazorPage()
        {
            LocalizationSourceName = internPJ3Consts.LocalizationSourceName;
        }
    }
}
