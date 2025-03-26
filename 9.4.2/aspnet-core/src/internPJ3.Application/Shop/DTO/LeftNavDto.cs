using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using internPJ3.Category;
using internPJ3.Products.DTO;


namespace internPJ3.Shop.DTO
{
	
	public class LeftNavDto : Entity<int>
	{
		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }

		public string ProductName { get; set; }
		public int ProductId { get; set; }

		public List<ProductDto> Products { get; set; } = new List<ProductDto>();

	}
}
