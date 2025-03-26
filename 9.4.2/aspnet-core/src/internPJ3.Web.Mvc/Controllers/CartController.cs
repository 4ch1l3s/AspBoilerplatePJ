using System.Threading.Tasks;
using System;
using internPJ3.Products;
using internPJ3.Products.DTO;
using internPJ3.Shop;
using internPJ3.Web.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using internPJ3.Web.Models.Cart;
using internPJ3.Cart;
using internPJ3.Controllers;
using Abp.Runtime.Session;
using internPJ3.Cart.DTO;

namespace internPJ3.Web.Controllers
{
	public class CartController : internPJ3ControllerBase
	{
		private readonly ICartAppService _cartService;
		private readonly IProductAppService _productAppService;
		private readonly IAbpSession _abpSession;

		public CartController(ICartAppService cartService, IProductAppService productAppService, IAbpSession abpSession)
		{
			_cartService = cartService;
			_productAppService = productAppService;
			_abpSession = abpSession;
		}


		//GetAll
		public async Task<ActionResult> Index() 
		{
			var output = await _cartService.GetAll();
		

			var model = new CartModel
			{
				Id = output.Id,
				CartStatus = output.CartStatus,
				CartItemList = output.CartItems,
				Coupon = output.Coupon,
				PhoneNumber = output.PhoneNumber,
				Address = output.Address 

			};
			return View(model);
		}


		//AddToCart
		[HttpPost]
		public async Task<IActionResult> AddToCart([FromBody] CreateCartItemListDto createCILDto)
		{
			if (createCILDto == null) return BadRequest("Invalid data");

			var userId = (int)(_abpSession.UserId ?? throw new UnauthorizedAccessException());
			int idCart;
			var cart = await _cartService.GetCart(userId);
			

			if (cart == null)
			{
				idCart = await _cartService.CreateCart();
			}
			else
			{
				idCart = cart.Id;
			}


			var cartItem = await _cartService.GetCartItem(createCILDto.ProductId, idCart);
			if (cartItem == null)
			{
				createCILDto.CartId = idCart;
				await _cartService.CreateCartItem(createCILDto);
			}
			else
			{
				await _cartService.UpdateCartItemQuantity(createCILDto.ProductId, createCILDto.OrderQuantity += createCILDto.AddQuantity, idCart);
			}

			return Ok("Item added to cart!");
		}




	}
}
