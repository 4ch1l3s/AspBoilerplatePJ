using internPJ3.Products.DTO;
using internPJ3.Roles.Dto;
using internPJ3.Product;
using internPJ3.Tasks1;
using System;
using System.Collections.Generic;
using internPJ3.Category.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace internPJ3.Web.Models.Product
{
	public class ProductModel
	{
		public IReadOnlyList<ProductListDto> ProductListDtos { get; set; } //list
		// IreadOnlyList => Interface trong C# đại diện cho 1 danh sách chỉ đọc
		// ProductListDto => kiểu dữ liệu của phần tử trong danh sách
		// Tên thuộc tính, được đặt theo kiểu dữ liệu của phần tử trong dsach
		// get - set => Cú pháp auto implement prop trong C# (get : đọc, set : update)
		//public List<SelectList> selectListItems { get; set; }

		public List<SelectListItem> CategoryList { get; set; } //list
		public int SelectedCategory { get; set; }

	}
}

