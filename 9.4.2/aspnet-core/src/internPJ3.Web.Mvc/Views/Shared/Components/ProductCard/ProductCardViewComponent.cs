using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using internPJ3.Web.Models;
using internPJ3.Products.DTO;
using internPJ3.Products;
using internPJ3.Web.Views.Shared.Components.AccountLanguages;
using internPJ3.Web.Models.Shop;

namespace internPJ3.Web.Views.Shared.Components.ProductCard
{
	public class ProductCardViewComponent : internPJ3ViewComponent
	{
		// Hàm InvokeAsync nhận dữ liệu sản phẩm và trả về View với dữ liệu đã truyền

		private readonly IProductAppService _productService;

		public ProductCardViewComponent(IProductAppService productService)
		{
			_productService = productService;
		}



		public async Task<IViewComponentResult> InvokeAsync(GetAllProductsDto input)
		{
			// Gọi bất đồng bộ phương thức _productService.GetAll(input)
			var output = (await _productService.GetAll(input)).Items;

			// Tạo model với danh sách sản phẩm
			var model = new ProductCardViewModel
			{
				ProductList = output,
			};

			// Trả về view với model
			return View(model);
		}


	}
}

