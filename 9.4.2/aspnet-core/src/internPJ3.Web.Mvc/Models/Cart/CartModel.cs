using internPJ3.Products.DTO;
using internPJ3.Roles.Dto;
using internPJ3.Product;
using internPJ3.Tasks1;
using System;
using System.Collections.Generic;
using internPJ3.Category.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using internPJ3.Shop.DTO;
using internPJ3.Cart.DTO;

namespace internPJ3.Web.Models.Cart
{
	public class CartModel
	{
		public int Id { get; set; }
		public long CartStatus { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public int Coupon { get; set; }

		public int TotalPrice { get; set; }
		public List<CartItemListDto> CartItemList { get; set; }
	}
}

