using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using internPJ3.Product;
using internPJ3.Tasks1;

namespace internPJ3.Products.DTO
{
	//[AutoMapFrom(typeof(TaskTest))]
	public class ProductListDto : EntityDto, IHasCreationTime
	{

		public productState ProductState { get; set; }

		public int? ProductQuantity { get; set; }

		[StringLength(ProductF.MaxDescriptionLength)]
		public string ProductDescription { get; set; }

		public DateTime CreationTime { get; set; }
		public int Id { get; set; }

		[Required]
		[StringLength(ProductF.MaxTitleLength)]
		public string ProductName { get; set; }

		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ProductPrice {  get; set; }
		public string ProductImagePath { get; set; }

		//public categordto cgf
	}
}
