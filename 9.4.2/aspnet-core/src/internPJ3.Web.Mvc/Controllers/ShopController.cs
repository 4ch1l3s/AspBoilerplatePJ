using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using internPJ3.Category;
using internPJ3.Controllers;
using internPJ3.Products;
using internPJ3.Products.DTO;
using internPJ3.Web.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using internPJ3.Shop.DTO;
using internPJ3.Category.DTO;
using internPJ3.Web.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using internPJ3.Shop;
using Microsoft.CodeAnalysis;

namespace internPJ3.Web.Controllers
{
	public class ShopController : internPJ3ControllerBase
	{
		private readonly IShopAppService _shopService;
		private readonly IProductAppService _productAppService;

		public ShopController(IShopAppService shopService, IProductAppService productAppService)
		{
			_shopService = shopService;
			_productAppService = productAppService;
		}
	

		public async Task<ActionResult> Index(GetAllProductsDto input, int pageSize = 8, int page = 1) //string searchString
		{
			input.PLocation = "Home"; //Truyền dữ liệu về cho service
			input.SkipCount = (page - 1) * pageSize;
			var outputItem = (await _shopService.GetAll(input)).Items;
			var output = await _shopService.GetAll(input);

			int totalProductCount = output.TotalCount;
			int totalPage = (int)Math.Ceiling((double)totalProductCount / pageSize);

			var leftNavResult = await _shopService.LeftNav(input);

			var model = new ShopModel
			{
				ProductList = outputItem,
				TotalPage = totalPage,
				CurrentPage = page,
				PageSize = pageSize,
				Categories = leftNavResult.Items.ToList()
			};
			return View(model);
		}


		//Get-All Products
		public async Task<ActionResult> Products(GetAllProductsDto input, int pageSize = 8, int page = 1)
		{
			input.PLocation = "Products"; //Truyền dữ liệu về cho service
			input.SkipCount = (page - 1) * pageSize;
			var outputItem = (await _shopService.GetAll(input)).Items;
			var output = await _shopService.GetAll(input);

			int totalProductCount = output.TotalCount;
			int totalPage = (int)Math.Ceiling((double)totalProductCount / pageSize);

			var leftNavResult = await _shopService.LeftNav(input);

			var model = new ShopModel
			{
				ProductList = outputItem,
				TotalPage = totalPage,
				CurrentPage = page,
				PageSize = pageSize,
				Categories = leftNavResult.Items.ToList()
			};
			return View(model);
		}
		//{
		//	input.PLocation = "Products";
		//	input.SkipCount = (page - 1) * pageSize;
		//	var output = await _shopService.GetAll(input);
		//	var outputItem = output.Items;
		//	var leftNavResult = await _shopService.LeftNav(input);

		//	int totalProductCount = output.TotalCount;
		//	int totalPage = (int)Math.Ceiling((double)totalProductCount / pageSize);

		//	var model = new ShopModel
		//	{
		//		ProductList = outputItem,
		//		TotalPage = totalPage,
		//		CurrentPage = page,
		//		PageSize = pageSize,
		//		Categories = leftNavResult.Items.ToList()
		//	};

		//	return View("_Products", model);
		//}

		//Details
		public async Task<IActionResult> Details(int Id)
		{
			
			var product = await _productAppService.Get(Id); //product này sẽ nhận về dto dựa trên giá trị Id của sản phẩm bằng phương thức get
			var model = new ShopModel
			{
				DetailsProductM = product,
			};

			return View("_Details", model);
		}






	}
}




