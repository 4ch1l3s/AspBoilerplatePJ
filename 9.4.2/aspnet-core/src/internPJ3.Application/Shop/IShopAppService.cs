using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using internPJ3.Category.DTO;
using internPJ3.Products.DTO;
using internPJ3.Shop.DTO;

namespace internPJ3.Shop
{
	public interface IShopAppService : IApplicationService
	{
		Task<PagedResultDto<ProductListDto>> GetAll(GetAllProductsDto input);

		Task<ListResultDto<LeftNavDto>> LeftNav(GetAllProductsDto input);
	}
}
