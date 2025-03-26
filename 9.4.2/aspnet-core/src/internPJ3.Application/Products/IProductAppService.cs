using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using internPJ3.Product;
using internPJ3.Products.DTO;
using internPJ3.Tasks.DTO;

namespace internPJ3.Products
{
	public interface IProductAppService : IApplicationService
	{
		//Task<ListResultDto<ProductListDto>> GetAll(GetAllProductsDto input);
		Task<PagedResultDto<ProductListDto>> GetAll(GetAllProductsDto input);
		Task<DetailsProductDto> Get(int id);

	}

	}
	