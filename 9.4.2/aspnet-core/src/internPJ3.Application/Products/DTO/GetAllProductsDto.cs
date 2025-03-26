using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using internPJ3.Product;
using internPJ3.Tasks1;
using Microsoft.Identity.Client;

namespace internPJ3.Products.DTO
{
	// sắp xếp : sorting - creationtime asc/ desc
	public class GetAllProductsDto :PagedAndSortedResultRequestDto
	{
		public TaskState? ProductState { get; set; }
		public string? ProductName { get; set; }
		public string? ProductDescription { get; set; }

		public int? ProductQuantity { get; set; }
		public int ProductPrice { get; set; }	
		public string ProductImagePath { get; set; }

		public int id { get; set; }
		
	
		public int? CategoryID { get; set; }
		public string CategoryName { get; set; }

		public string SearchString { get; set; }

		//public int SkipCount {  get; set; }
		public int PageSize { get; set; } = 8;

		public string PLocation { get; set; }
	}
}
