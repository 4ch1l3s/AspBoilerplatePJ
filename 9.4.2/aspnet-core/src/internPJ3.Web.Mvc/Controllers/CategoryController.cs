using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using internPJ3.Authorization;
using internPJ3.Category;
using internPJ3.Category.DTO;
using internPJ3.Controllers;
using internPJ3.Products;
using internPJ3.Products.DTO;
using internPJ3.Web.Models.Category;
using internPJ3.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace internPJ3.Web.Controllers
{
	[AbpMvcAuthorize(PermissionNames.Pages_Category)]
	public class CategoryController : internPJ3ControllerBase {

		private readonly ICategoryAppService _categoryService;

		public CategoryController(ICategoryAppService categoryService)

		{
			_categoryService = categoryService;
		}

		//Xử lý Get-All
		public async Task<ActionResult> Index(CategoryGetAllDto input)
		{
			var output = (await _categoryService.GetAll(input)).Items;
			var model = new CategoryModel
			{
				CategoryListDto = output
			};
			return View(model);
		}

		//Xử lý Edit
		public async Task<ActionResult> EditModal(int Id)
		{
			var category = await _categoryService.Get(Id);
			var model = new CategoryEditModel
			{
				CategoryEditDto = category,
			};
			return PartialView("_EditModal", model);
		}





		//public IActionResult Index()
		//{
		//	return View();
		//}
	}
}
