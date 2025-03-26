using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using internPJ3.Controllers;

namespace internPJ3.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : internPJ3ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
