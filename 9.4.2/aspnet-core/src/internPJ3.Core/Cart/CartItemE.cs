using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using internPJ3.Product;


namespace internPJ3.Cart
{

	[Table("CartItems")]
	public class CartItemE : Entity<int>

	{
		public int OrderQuantity { get; set; }

		[ForeignKey(nameof(ProductId))]
		public ProductF ProductF { get; set; }
		public int ProductId { get; set; }
		

		[ForeignKey(nameof(CartId))] // Mục này phải nằm phía trên Navigation Prop
		public CartE CartE { get; set; } // Navigation Property 
		public int CartId { get; set; } // Foreign Key
		

	}

}
