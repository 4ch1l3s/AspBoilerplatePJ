using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using internPJ3.Product;

namespace internPJ3.Products.DTO
{

	public class ProductDto : EntityDto<int>, IHasCreationTime
	{
		public productState ProductState { get; set; }
		public int ProductPrice { get; set; }
		public int? ProductQuantity { get; set; }
		public string ProductDescription { get; set; }
		public string ProductName { get; set; }
		public DateTime CreationTime { get; set; }
	}
}
