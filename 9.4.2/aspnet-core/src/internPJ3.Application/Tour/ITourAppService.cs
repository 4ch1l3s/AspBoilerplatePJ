using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using internPJ3.Tour;
using internPJ3.Tour.DTO;
using internPJ3.Tasks.DTO;
using internPJ3.Products.DTO;

namespace internPJ3.Tour
{
	public interface ITourAppService : IApplicationService
	{
		//Task<ListResultDto<ProductListDto>> GetAll(GetAllProductsDto input);
		Task<PagedResultDto<TourListDto>> GetAll(TourGetAllDto input);
		Task<TourUpdateDto> Get(int id);
	}

	}
	