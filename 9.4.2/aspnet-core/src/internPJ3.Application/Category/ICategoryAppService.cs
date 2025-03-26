using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using internPJ3.Category.DTO;
using internPJ3.Products.DTO;

namespace internPJ3.Category
{
	public interface ICategoryAppService : IApplicationService
	{
		Task<PagedResultDto<CategoryListDto>> GetAll(CategoryGetAllDto input);
		Task<CategoryEditDto> Get(int id);
	}
}
