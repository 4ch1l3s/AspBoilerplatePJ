using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using internPJ3.Products;
using internPJ3.Category;
using internPJ3.Shop.DTO;
using internPJ3.Product;
using Abp.Linq.Extensions;
using internPJ3.Products.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Filters;

namespace internPJ3.Shop
{
	public class ShopAppService : internPJ3AppServiceBase, IShopAppService
	{
		private readonly IRepository<ProductF> _productRepository;
		private readonly IRepository<Categories> _categoriesRepository;

		public ShopAppService(IRepository<ProductF> productRepository, IRepository<Categories> categoriesRepository)
		{
			_productRepository = productRepository;
			_categoriesRepository = categoriesRepository;
		}

		// Đọc all - R
		public async Task<PagedResultDto<ProductListDto>> GetAll(GetAllProductsDto input) //Dữ liệu trả về là danh sách sản phẩm được gói trong đối tượng ListResultDto
		{
			var products = _productRepository.GetAll();//sách products từ database


			//Logic của Search
			if (!string.IsNullOrEmpty(input.SearchString))
			{
				products = products.Where(n => n.ProductName.Contains(input.SearchString)); //Chỉ tìm kiếm theo tên
			}

		
			//Nối dữ liệu Category
			products = products.Include(t => t.Categories);

			var TotalCount = await products.CountAsync(); //đếm dữ liệu có trong bảng

			//Sắp xếp thứ tự
			products = (input.PLocation == "Products") ? products.OrderBy(x => x.Id) : products.OrderByDescending(x => x.Id);



			var items = await products
					.Skip(input.SkipCount) // Bỏ qua số lượng sản phẩm tương ứng với trang hiện tại
					.Take(input.PageSize) // Giới hạn số sản phẩm trả về mỗi lần theo pageSize
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

			return new PagedResultDto<ProductListDto>(TotalCount,items);
		}

		//LeftNav
		public async Task<ListResultDto<LeftNavDto>> LeftNav(GetAllProductsDto input)
		{
			var products = await _productRepository.GetAll()
					.Include(x => x.Categories) // Chỉ Include nếu Product có 1 Category
					.Select(x => new
					{
						CategoryId = x.Categories.Id,
						CategoryName = x.Categories.CategoryName,
						ProductName = x.ProductName,
						Id = x.Id
					})
					.ToListAsync();

			// Nhóm theo danh mục
			var groupedCategories = products
					.GroupBy(x => new { x.CategoryId, x.CategoryName })
					.Select(g => new LeftNavDto
					{
						CategoryId = g.Key.CategoryId,
						CategoryName = g.Key.CategoryName,
						Products = g.Select(p => new ProductDto { ProductName = p.ProductName, Id = p.Id }).ToList()
					})
					.ToList();

			return new ListResultDto<LeftNavDto>(groupedCategories);
		}


		//Details
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

	}
}
