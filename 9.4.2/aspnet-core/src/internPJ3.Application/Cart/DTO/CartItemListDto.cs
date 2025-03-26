using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using internPJ3.Product;
using internPJ3.Products.DTO;
using Microsoft.AspNetCore.Http;

namespace internPJ3.Cart.DTO
{
	public class CartItemListDto : EntityDto
	{
		public int ProductPrice { get; set; }
		public int OrderQuantity { get; set; }
		public string ProductImagePath { get; set; }
		public int SubTotalProductPrice { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }

		public int? ProductQuantity { get; set; }
		public int CartId { get; set; }
		public long CartStatus {  get; set; }
		public List<DetailsProductDto> ListProducts { get; set; }

	}
}
