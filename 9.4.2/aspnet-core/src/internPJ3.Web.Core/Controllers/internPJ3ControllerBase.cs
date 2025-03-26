using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace internPJ3.Controllers
{
    public abstract class internPJ3ControllerBase: AbpController
    {
        protected internPJ3ControllerBase()
        {
            LocalizationSourceName = internPJ3Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
