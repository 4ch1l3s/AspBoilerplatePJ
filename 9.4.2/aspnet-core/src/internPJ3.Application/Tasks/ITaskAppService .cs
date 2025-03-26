using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Application.Services;
using internPJ3.Tasks.DTO;
using internPJ3.Products.DTO;

namespace internPJ3.Tasks
{
	public interface ITaskService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input); 
        Task Create(CreateTaskInput input);
        Task Update(TaskListDto input);
        Task Delete(int Id);
		Task GetAll(GetAllProductsDto input);
	}
}
