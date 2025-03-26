using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using internPJ3.Product;


namespace internPJ3.Cart
{
 
	public class CartItemE : Entity<int>

	{
		public int OrderQuantity { get; set; }

		[ForeignKey(nameof(ProductId))]
		public int ProductId { get; set; }
		public ProductF ProductF { get; set; }

		[ForeignKey(nameof(CartId))]
		public int CartId { get; set; } // Foreign Key
		public CartE CartE { get; set; } // Navigation Property 

	}

}
