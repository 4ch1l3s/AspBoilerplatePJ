using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using internPJ3.Product;
using Microsoft.AspNetCore.Http;

namespace internPJ3.Cart.DTO
{
	public class CreateCartItemListDto : EntityDto
	{
		public int OrderQuantity { get; set; }
		public int AddQuantity { get; set; }
		public string ProductImagePath { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int? ProductQuantity { get; set; }
		public int CartId { get; set; }
		public long CartStatus {  get; set; }
		
	}
}
