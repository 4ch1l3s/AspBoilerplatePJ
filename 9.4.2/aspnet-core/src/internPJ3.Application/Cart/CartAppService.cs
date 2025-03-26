using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using internPJ3.Authorization;
using internPJ3.EntityFrameworkCore;
using internPJ3.Product;
using internPJ3.Products.DTO;
using internPJ3.Tasks.DTO;
using internPJ3.Tasks1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using Abp.Runtime.Session;
using internPJ3.Users.Dto;
using internPJ3.Cart;
using internPJ3.Cart.DTO;
using internPJ3.Authorization.Users;
using internPJ3.Products;
using System.Security.Cryptography.X509Certificates;



namespace internPJ3.Cart
{
	
	public class CartAppService : internPJ3AppServiceBase, ICartAppService
	{
		private readonly IRepository<CartE> _cartRepository; // Giúp làm việc với database mà không cần SQL Query
		private readonly IRepository<CartItemE> _cartItemRepository;
		private readonly IRepository<ProductF> _productRepository;
		private readonly IAbpSession _abpSession;

		public CartAppService(IRepository<CartE> cartRepository, IRepository<CartItemE> cartItemRepository, IAbpSession abpSession, IRepository<ProductF> productRepository) // Thiết lập để TaskAppService có thể làm việc với dữ liệu ProductF
																																																																																				 // thông qua _taskRepository - ChatGPT
		{
			_cartRepository = cartRepository;
			_cartItemRepository = cartItemRepository;
			_abpSession = abpSession;
			_productRepository = productRepository;
		}


		//Create new cart, bắt đăng nhập - thêm sau
		public async Task<int> CreateCart()
		{

			if (!_abpSession.UserId.HasValue)
			{
				throw new Exception("Must login to create cart");
			}

			CartE CE = new CartE(); //Tự động gen Id
			CE.UserId = (int)(_abpSession.UserId ?? 0); //_abpSession.UserId trả về giá trị kiểu long? nên phải ép kiểu về int
			CE.CartStatus = 1;
			CE.CreationTime = DateTime.Now;
			var id = await _cartRepository.InsertAndGetIdAsync(CE);
			return id;
		}

		//Create Cart Item
		public async Task CreateCartItem(CreateCartItemListDto input)
		{
			if (!_abpSession.UserId.HasValue)
			{
				throw new Exception("Must login to create cart item");
			}
			var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == input.ProductId); //Lấy ra product trùng với product Id nhận vào từ input
			CartItemE cartItemE = new CartItemE(); //Tự động gen Id
			cartItemE.ProductId = product.Id;
			cartItemE.OrderQuantity = 1;
			cartItemE.CartId = input.CartId; //?
			
			await _cartItemRepository.InsertAsync(cartItemE);
		}

		//Update cart (user)
		public async Task UpdateCart(UpdateCartDto input)
		{
			var userId = _abpSession.UserId ?? throw new UnauthorizedAccessException(); //Lấy userId
			var cart = await _cartRepository.FirstOrDefaultAsync(c => c.UserId == userId && c.CartStatus == 1); //Lấy ra cart từ server, có userId tương ứng và Status = 1

			cart.CartStatus = input.CartStatus;
			cart.Address = input.Address;
			cart.PhoneNumber = input.PhoneNumber;
			cart.Coupon = input.Coupon;

			await _cartRepository.UpdateAsync(cart);
		}

		//Delete cart (user) - xem lai
		public async Task DeleteCart(int Id)
		{
			var userId = _abpSession.UserId ?? throw new UnauthorizedAccessException(); //Lấy userId
			var cart = await _cartRepository.FirstOrDefaultAsync(c => c.UserId == userId && c.CartStatus == 1); //Lấy ra cart từ server, có userId tương ứng và Status = 1
		
			await _cartRepository.DeleteAsync(Id);
		}

		//GetAll (user page)
		public async Task<GetAllCartDto> GetAll()
		{
			
			var userId = _abpSession.UserId ?? throw new UnauthorizedAccessException(); //Lấy userId
			var cart = await _cartRepository.FirstOrDefaultAsync(c => c.UserId == userId && c.CartStatus == 1); //query có điều kiện ra cart
			
			var cartItems = _cartItemRepository.GetAll().Include(cil => cil.ProductF).Include(cil => cil.CartE).Where(cil => cil.CartE.Id == cart.Id); //query danh sách cartItem (điều kiện cart.Id khớp) -> chuyển sang dạng List
			var itemList = await cartItems
			.Select(v => new CartItemListDto
			{
				OrderQuantity = v.OrderQuantity,
				ProductImagePath = v.ProductF.ProductImagePath,
				ProductId = v.ProductF.Id,
				ProductName = v.ProductF.ProductName,
				ProductQuantity = v.ProductF.ProductQuantity,
				ProductPrice = v.ProductF.ProductPrice
			}
		).ToListAsync();

			var TotalCount =  itemList.Count();

			var GetAll = new GetAllCartDto
			{
				CartStatus = cart.CartStatus,
				Address = cart.Address,
				PhoneNumber = cart.PhoneNumber,
				Coupon = cart.Coupon,
				CartItems = itemList
			};
			return GetAll;
		}

		//GetCart (user page)
		public async Task<CartE> GetCart(int userId)
		{
			var cart = await _cartRepository.FirstOrDefaultAsync(c => c.UserId == userId && c.CartStatus == 1);
			if (cart == null) { return null; }
			return cart;
		}

		//GetCartItem (user page)
		public async Task<CartItemE> GetCartItem(int ProductId, int CartId)
		{
			var cartItem = await _cartItemRepository.FirstOrDefaultAsync(c => c.ProductId == ProductId && c.CartId == CartId);
			return cartItem;
		}


		//Update Cart Item Quantity
		public async Task UpdateCartItemQuantity(int ProductId, int NewQuantity, int CartId)
		{
			if (!_abpSession.UserId.HasValue)
			{
				throw new Exception("Must login to update cart item");
			}
			var result = await _cartItemRepository.FirstOrDefaultAsync(c => c.ProductId == ProductId && c.CartId == CartId); //Không sử dụng GetAll vì GetAll không hỗ trợ tìm theo điều kiện, điều kiện hiệ tại là tìm theo ProductId
			result.OrderQuantity = NewQuantity;
			await _cartItemRepository.UpdateAsync(result); 
		}
	}
}
