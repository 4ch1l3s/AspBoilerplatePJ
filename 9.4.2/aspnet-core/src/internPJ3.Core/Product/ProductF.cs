using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using internPJ3.Category;
using internPJ3.Person1;
using internPJ3.Tasks1;
using Microsoft.AspNetCore.Http;

namespace internPJ3.Product
{
	[Table("Product")]

	public class ProductF : Entity, IHasCreationTime
	{
		public const int MaxTitleLength = 256;
		public const int MaxDescriptionLength = 64 * 1024;

		[Required]
		[StringLength(MaxTitleLength)]
		public string ProductName { get; set; }

		[StringLength(MaxDescriptionLength)]
		public string ProductDescription { get; set; }

		public productState ProductState { get; set; }

		public int? ProductQuantity { get; set; }

		public DateTime CreationTime { get; set; }

		public int ProductPrice { get; set; }

		public string? ProductImagePath { get; set; }

		public ProductF()
		{
			ProductState = productState.Selling;
			CreationTime = Clock.Now;
		}

		public ProductF(string pn, string pd = null, int? pq = null, int ProductCategoryId =0)
						: this ()
		{
			ProductName = pn;
			ProductDescription = pd;
			ProductQuantity = pq;
			CategoryId = ProductCategoryId;
		}

		[ForeignKey(nameof(CategoryId))]
		public Categories Categories { get; set; } //Navigation Prop
		public int? CategoryId { get; set; } //Khóa ngoại


	}
	public enum productState : byte
	{
		Selling = 0,
		OutStock = 1
	}
}
