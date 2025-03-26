using internPJ3.Products.DTO;
using internPJ3.Roles.Dto;
using internPJ3.Product;
using internPJ3.Tasks1;
using System;
using System.Collections.Generic;
using internPJ3.Category.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using internPJ3.Shop.DTO;

namespace internPJ3.Web.Models.Shop
{
	public class ShopModel
	{
	
		public string CategoryName { get; set; }
		public int? CategoryId { get; set; }

		public IReadOnlyList<ProductListDto> ProductList { get; set; }

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public string ProductImagePath { get; set; }
		public int ProductPrice { get; set; }

		public int TotalPage { get; set; }
		public int CurrentPage { get; set; }
		public string PageLocation { get; set; }
		public int PageSize { get; set; }

		public DetailsProductDto DetailsProductM { get; set; }

		public List<LeftNavDto> Categories { get; set; }
	}
}

