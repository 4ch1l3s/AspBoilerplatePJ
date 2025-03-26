using internPJ3.Products.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace internPJ3.Web.Models.Product
{
	public class EditProductModel
	{
		public DetailsProductDto EditProductM  { get; set; }
		public List<SelectListItem> CategoryList { get; set; }
		public int? SelectedCategory { get; set; }
		public string filePath { get; set; }
	}
}
