using internPJ3.Tasks.DTO;
using internPJ3.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using internPJ3.Web.Models.Product;
using internPJ3.Web.Models.Task;
using internPJ3TaskApp.Tasks;
using internPJ3.Products.DTO;
using internPJ3.Controllers;
using internPJ3.Products;
using Abp.Application.Services.Dto;
using System;
using Microsoft.AspNetCore.Hosting;
using internPJ3.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using internPJ3.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using internPJ3.Web.Models.Category;
using internPJ3.Category.DTO;
using Abp.AspNetCore.Mvc.Authorization;
using internPJ3.Authorization;

namespace internPJ3.Web.Controllers
{
	[AbpMvcAuthorize(PermissionNames.Pages_Products)]
	public class ProductController : internPJ3ControllerBase
	{

		private readonly IProductAppService _productService;
		private readonly ICategoryAppService _categoryAppService;

		public ProductController(IProductAppService productService, ICategoryAppService categoryAppService)
		{
			_productService = productService;
			_categoryAppService = categoryAppService;
		}

		//Get-All
		public async Task<ActionResult> Index(GetAllProductsDto input, CategoryGetAllDto input2) //string searchString
		{
			var output = (await _productService.GetAll(input)).Items;
			var cate = (await _categoryAppService.GetAll(input2)).Items; //selectDropList

			

			//logic xử lý Category Dropbox
			List<SelectListItem> list = new List<SelectListItem>();
			foreach (var item in cate)
			{
				SelectListItem selectListItem = new SelectListItem();
				selectListItem.Text = @L(item.CategoryName);
				selectListItem.Value = item.Id.ToString(); //Vì Value không nhận int
				list.Add(selectListItem);
			}

			var model = new ProductModel
			{
				ProductListDtos = output,
				CategoryList = list
			};
			return View(model);
		}


		//Edit
		public async Task<ActionResult> EditModal(int Id, CategoryGetAllDto input2) //rì quét từ phía giao diện
		{


			var product = await _productService.Get(Id); //product này sẽ nhận về dto dựa trên giá trị Id của sản phẩm bằng phương thức get
			var cate = (await _categoryAppService.GetAll(input2)).Items; //selectDropList


			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", product.ProductImagePath);

			//dropdown Category
			List<SelectListItem> list = new List<SelectListItem>();
			foreach (var item in cate)
			{
				SelectListItem selectListItem = new SelectListItem();
				selectListItem.Text = item.CategoryName;
				selectListItem.Value = item.Id.ToString();
				list.Add(selectListItem);
			}


			var model = new EditProductModel //Tạo biến model để búng giá trị sang cho View, EditProductModel là 1 ViewModel chứa thông tin hiển thị giao diện
			{
				EditProductM = product, // Truyền dữ liệu vừa bếch được nhờ Service sang Model của View (EditProductM) 
				CategoryList = list,
				SelectedCategory = product.CategoryId,
				filePath = filePath,
			};
			return PartialView("_EditModal", model);  //Nạp file _EditModal - chứa giao diện chỉnh sửa
																								//Truyền model vào View để hiển thị thông tin sản phẩm


		}


		//Get 1 modal
		public async Task<ActionResult> DetailsModal(int Id)
		{

			var product = await _productService.Get(Id); //product này sẽ nhận về dto dựa trên giá trị Id của sản phẩm bằng phương thức get
			var model = new DetailsProductModel
			{
				DetailsProductM = product,
			};

			return PartialView("_DetailsModal", model);
		}





		//Img Create + Edit
		[HttpPost]
		public async Task<ActionResult> UploadImage(IFormFile ProductImage)
		{

			if (ProductImage == null || ProductImage.Length == 0) //Kiểm tra file
			{
				return Json(new { success = false, message = "Không có file được upload" });
			}
			//Validate Backend
			var fileExtension = Path.GetExtension(ProductImage.FileName).ToLowerInvariant(); //Chuyển thành chữ thường
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; //setup đuôi file hợp lệ
			if (!allowedExtensions.Contains(fileExtension))
			{
				return Json(new { success = false, message = "Định dạng file không hợp lệ" });
			}

			//var newFileName = Guid.NewGuid().ToString() + fileExtension;
			var newFileName = ProductImage.FileName; // Giữ nguyên tên file cũ, dùng cho việc ghi đè
			var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

			var filePath = Path.Combine(uploadPath, newFileName); //khởi tại filePath mới

			using (FileStream stream = new(filePath, FileMode.Create)) //lưu file ảnh
			{
				await ProductImage.CopyToAsync(stream);
			}

			var fileUrl = Url.Content("~/uploads/" + newFileName);
			return Json(new { success = true, filePath = fileUrl });
		}










		//public async Task<ActionResult> Index(GetAllTasksInput input)
		//{
		//	var categpry = (await categoryservice.GetAll(input)).Items;
		//foreach(cate){
		// goi danh sach product dua vao category.Id
	//}
		//	var model = new TaskModel
		//	{
		//		TaskListDtos = output
		//	};
		//	return View(model);
		//}


	}
}

