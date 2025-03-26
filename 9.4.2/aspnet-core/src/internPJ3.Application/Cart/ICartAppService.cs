using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using internPJ3.Cart.DTO;
using internPJ3.Product;
using internPJ3.Products.DTO;
using internPJ3.Tasks.DTO;

namespace internPJ3.Cart
{
	public interface ICartAppService : IApplicationService
	{
		Task<GetAllCartDto> GetAll();
		Task<CartE> GetCart(int id);
		Task<CartItemE> GetCartItem(int ProductId, int CartId);
		Task<int> CreateCart();
		Task CreateCartItem(CreateCartItemListDto input);
		Task UpdateCartItemQuantity(int ProductId, int NewQuantity, int CartId);
		//Task<PagedResultDto<ProductListDto>> GetAll(GetAllProductsDto input);
		//Task<DetailsProductDto> Get(int id);

	}

	}
	