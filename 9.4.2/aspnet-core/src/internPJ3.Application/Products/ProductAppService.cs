using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using internPJ3.Authorization;
using internPJ3.Product;
using internPJ3.Products.DTO;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using Abp.Runtime.Session;
using internPJ3.Users.Dto;



namespace internPJ3.Products
{
	
	[AbpAuthorize(PermissionNames.Pages_Products)]
	public class ProductAppService : internPJ3AppServiceBase, IProductAppService
	{
		private readonly IRepository<ProductF> _productRepository; // Giúp làm việc với database mà không cần SQL Query



		public ProductAppService(IRepository<ProductF> productRepository) // Thiết lập để TaskAppService có thể làm việc với dữ liệu ProductF
																																			// thông qua _taskRepository - ChatGPT
		{
			_productRepository = productRepository;

		}



		//Create new Product
		public async Task Create(CreateProductDto input)
		{
			//var task = ObjectMapper.Map<TaskTest>(input); - Hiện không dùng automap
			ProductF Pf = new ProductF(); //khởi tạo enity mới
			Pf.ProductName = input.ProductName;
			Pf.ProductDescription = input.ProductDescription;
			Pf.ProductState = input.ProductState; // Điều chỉnh trong Edit nếu cần, code cứng = 0 - Selling
			Pf.CreationTime = DateTime.Now;
			Pf.ProductQuantity = input.ProductQuantity;
			Pf.ProductImagePath = input.ProductImagePath;
			Pf.CategoryId = input.CategoryId;
			Pf.ProductPrice = input.ProductPrice;
			await _productRepository.InsertAsync(Pf);
		}




		// R-All-Render All Products
		public async Task<PagedResultDto<ProductListDto>> GetAll(GetAllProductsDto input) //Dữ liệu trả về là danh sách sản phẩm được gói trong đối tượng ListResultDto
		{
			var products = _productRepository.GetAll();//sách products từ database

			
			//Logic của Search
			if (!string.IsNullOrEmpty(input.SearchString))
			{
				products = products.Where(n => n.ProductName.Contains(input.SearchString) ||
																			 n.ProductDescription.Contains(input.SearchString));
			}

			//Nối dữ liệu Category
			products = products.Include(t => t.Categories);


			var TotalCount = await products.CountAsync(); //đếm dữ liệu có trong bảng

			

			var items = await products
				.OrderBy(x => x.Id)
				.PageBy(input)
				.Select(x => new ProductListDto
			{
				ProductPrice = x.ProductPrice,
				ProductImagePath = x.ProductImagePath,
				ProductName = x.ProductName,
				ProductQuantity = x.ProductQuantity,
				ProductDescription = x.ProductDescription,
				CreationTime = x.CreationTime,
				ProductState = x.ProductState,
				CategoryName = x.Categories.CategoryName,
				CategoryId = x.CategoryId,
				Id = x.Id,
			})
			.ToListAsync();


			return new PagedResultDto<ProductListDto>(TotalCount,
					items
			);
		}


		//Read a Product (Details)
		public async Task<DetailsProductDto> Get(int id)
		{
			var result = await _productRepository.GetAsync(id);
			DetailsProductDto productDto = new DetailsProductDto();
			productDto.Id = result.Id;
			productDto.ProductName = result.ProductName;
			productDto.ProductDescription = result.ProductDescription;
			productDto.ProductState = result.ProductState;
			productDto.CreationTime = result.CreationTime;
			productDto.ProductQuantity = result.ProductQuantity;
			productDto.ProductImagePath = result.ProductImagePath;
			productDto.CategoryId = result.CategoryId;
			productDto.ProductPrice = result.ProductPrice;
			//productDto.CategoryId = result.CategoryId;
			//productDto.CategoryName = result.CategoryNP.CategoryName;

			return productDto;
		}


		//Edit Product

		[AbpAuthorize(PermissionNames.Edit_Perm)]
		public async Task Update(EditProductDto input) // Nhận vào 1 Dto bao gồm nhiều trường khác nhau
		{
			var product = await _productRepository.GetAsync(input.Id); //Từ id của Dto input, ta tìm được giá trị cũ của nó trong server, trả về dto
																																 //var task = await _taskRepository.FirstOrDefaultAsync(x=>x.Id == input.Id);
			product.ProductName = input.ProductName;                    ///////////////////////////
			product.ProductDescription = input.ProductDescription;      //   TIẾN HÀNH THAY      //
			product.ProductState = input.ProductState;                  //   THẾ GIÁ TRỊ CŨ      //
			product.ProductQuantity = input.ProductQuantity;            ///////////////////////////
			product.ProductImagePath = input.ProductImagePath;
			product.CategoryId = input.CategoryId;
			product.ProductPrice = input.ProductPrice;
			await _productRepository.UpdateAsync(product); // CẬP NHẬT GIÁ TRỊ DTO MỚI
		}





		//public async Task<ProductListDto> Get(EntityDto<int> input)
		//{
		//	var result = await _productRepository.GetAsync(input.Id);
		//	ProductListDto productListDto = new ProductListDto();
		//	productListDto.Id = result.Id;
		//	productListDto.ProductName = result.ProductName;
		//	productListDto.ProductDescription = result.ProductDescription;
		//	productListDto.ProductState = result.ProductState;
		//	productListDto.CreationTime = result.CreationTime;
		//	return productListDto;
		//}




		//Delete Product



		[AbpAuthorize(PermissionNames.Delete_Perm)]
		public async Task Delete(int Id)
		{
			await _productRepository.DeleteAsync(Id);
		}


		//Delete Img
		public async Task DeleteImg(int Id, string filePath)
		{
			// Lấy đường dẫn ảnh từ database
			var Products = await _productRepository.GetAsync(Id);
			string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));


			// Đường dẫn file trên server
			System.IO.File.Delete(absolutePath); //Xóa file
			Products.ProductImagePath = ""; //Xóa dữ liệu ảnh khỏi database
			await _productRepository.UpdateAsync(Products);
		}


		//Lang
		public async Task ChangeLanguage(ChangeUserLanguageDto input)
		{
			await SettingManager.ChangeSettingForUserAsync(
					AbpSession.ToUserIdentifier(),
					LocalizationSettingNames.DefaultLanguage,
					input.LanguageName
			);
		}






	}
}
