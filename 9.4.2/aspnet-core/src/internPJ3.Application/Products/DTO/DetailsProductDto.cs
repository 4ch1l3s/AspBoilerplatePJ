using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internPJ3.Category;
using internPJ3.Product;
using Microsoft.AspNetCore.Http;

namespace internPJ3.Products.DTO
{
	public class DetailsProductDto
	{
		public int ProductPrice { get; set; }
		public productState ProductState { get; set; }

		public int? ProductQuantity { get; set; }

		[StringLength(ProductF.MaxDescriptionLength)]
		public string ProductDescription { get; set; }

		public DateTime CreationTime { get; set; }
		public int Id { get; set; }

		[Required]
		[StringLength(ProductF.MaxTitleLength)]
		public string ProductName { get; set; }

		public string? ProductImagePath { get; set; }

		[NotMapped] // Không ánh xạ vào database
		public IFormFile? ProductImage { get; set; }

		public int? CategoryId { get; set; }

		//public Categories CategoryNP { get; set; }

		public string CategoryName { get; set; }

	}
}
