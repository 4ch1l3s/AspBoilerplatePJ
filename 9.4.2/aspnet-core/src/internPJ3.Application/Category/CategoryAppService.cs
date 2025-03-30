using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using internPJ3.Authorization;
using internPJ3.Category;
using internPJ3.Category.DTO;
using internPJ3.Product;
using internPJ3.Products.DTO;
using Microsoft.EntityFrameworkCore;


namespace internPJ3.Category

{
	[AbpAuthorize(PermissionNames.Pages_Category)]
	public class CategoryAppService : internPJ3AppServiceBase, ICategoryAppService
	{
		private readonly IRepository<Categories> _categoryRepository;
		private readonly IRepository<ProductF> _productRepository;

		public CategoryAppService(IRepository<Categories> categoryRepository, IRepository<ProductF> productRepository)

		{
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
		}


		//Get-All
		public async Task<PagedResultDto<CategoryListDto>> GetAll(CategoryGetAllDto input) //Dữ liệu trả về là danh sách sản phẩm được gói trong đối tượng ListResultDto
		{
			var category = await _categoryRepository.GetAllAsync();  //sách products từ database
			var product = await _productRepository.GetAllAsync();

			//Logic của Search
			if (!string.IsNullOrEmpty(input.SearchString))
			{
				category = category.Where(n => n.CategoryDescription.Contains(input.SearchString) ||
																			 n.CategoryName.Contains(input.SearchString));
			}


			var TotalCount = await category.CountAsync(); //đếm dữ liệu có trong bảng


			input.Sorting = "CreationTime DESC";

			var items = await category.PageBy(input)
				.Select(x => new CategoryListDto
				{
					CategoryName = x.CategoryName,
					CategoryDescription = x.CategoryDescription,
					Id = x.Id,
					ProductCount = product.Count(p => p.CategoryId == x.Id)
				})
			.ToListAsync();



			return new PagedResultDto<CategoryListDto>(TotalCount,
					items
			);
		}


		//Create
		public async Task Create(CategoryDto input)
		{
			//var task = ObjectMapper.Map<TaskTest>(input); - Hiện không dùng automap
			Categories Cf = new Categories(); //khởi tạo enity mới
			Cf.CategoryName = input.CategoryName;
			Cf.CategoryDescription = input.CategoryDescription;
			Cf.Id = input.Id;
			await _categoryRepository.InsertAsync(Cf);
		}
		//Get
		public async Task<CategoryEditDto> Get(int id)
		{
			var input = await _categoryRepository.GetAsync(id);
			



			CategoryEditDto categoryEditDto = new CategoryEditDto();
			
			categoryEditDto.Id = input.Id;
			categoryEditDto.CategoryName = input.CategoryName;
			categoryEditDto.CategoryDescription = input.CategoryDescription;
			return categoryEditDto;
		}



		
		[AbpAuthorize(PermissionNames.Delete_Perm)]
		//Delete
		public async Task Delete(int Id)
		{
			// Lấy danh sách sản phẩm thuộc categoryId này
			var productCount = await _productRepository.CountAsync(p => p.CategoryId == Id);
			if (productCount != 0) {
				return; // Bổ sung báo lỗi
			}
			await _categoryRepository.DeleteAsync(Id);
		}

		// Cập nhật lại các sản phẩm về danh mục mặc định - Sai logic
		//foreach (var product in products)
		//{
		//	product.CategoryId = 1;
		//	await _productRepository.UpdateAsync(product);
		//}





		[AbpAuthorize(PermissionNames.Edit_Perm)]
		//Edit
		public async Task Update(CategoryEditDto input)
		{
			var category = await _categoryRepository.GetAsync(input.Id);
			category.CategoryName = input.CategoryName;
			category.CategoryDescription = input.CategoryDescription;
			category.Id = input.Id;
			await _categoryRepository.UpdateAsync(category);

		}
	}
}
