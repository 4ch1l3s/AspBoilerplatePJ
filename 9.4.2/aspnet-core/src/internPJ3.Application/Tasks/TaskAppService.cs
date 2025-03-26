
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.Internal.Mappers;
using internPJ3.Tasks.DTO;
using internPJ3.Tasks;
using Microsoft.EntityFrameworkCore;
using internPJ3;
using internPJ3.Tasks1;
using System.Threading.Tasks;
using System;
using internPJ3.Products.DTO;
using internPJ3.Products;

namespace internPJ3TaskApp.Tasks
{
	public class TaskAppService : internPJ3AppServiceBase
	{
		private readonly IRepository<TaskTest> _taskRepository; // Giúp làm việc với database mà không cần SQL Query

		public TaskAppService(IRepository<TaskTest> taskRepository) // Thiết lập để TaskAppService có thể làm việc với dữ liệu TaskTest
																																// thông qua _taskRepository - ChatGPT
		{
			_taskRepository = taskRepository;
		}

		//Đọc all - R
		public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput results)
		{
			var tasks = await _taskRepository //truy vấn danh
					.GetAll()                     //sách tasks từ database

					.Include(t => t.AssignedPerson) //bao gồm thông tin người được giao task
					.WhereIf(results.State.HasValue, t => t.State == results.State.Value) //Lọc theo trạng thái (nếu có)
					.OrderByDescending(t => t.CreationTime) //Sắp xếp theo thời gian tạo
					.Select(x=> new TaskListDto
					{
						Title = x.Title,	
						Description = x.Description,
					}) 
					.ToListAsync();  //chuyển kết quả thành danh sách

			return new ListResultDto<TaskListDto>(
				tasks
			);
		}

			
			//Ghi - C
			public async Task Create(CreateTaskInput input)
			{
				//var task = ObjectMapper.Map<TaskTest>(input);

				TaskTest taskTest = new TaskTest(); //khởi tạo enity mới
				taskTest.Title = input.Title;
				taskTest.Description = input.Description;
				taskTest.State = input.State;
				taskTest.CreationTime = DateTime.Now;
				await _taskRepository.InsertAsync(taskTest);
			}


			//Sửa - U
			public async Task Update(TaskListDto input)
			{
				var task = await _taskRepository.GetAsync(input.Id);
			//var task = await _taskRepository.FirstOrDefaultAsync(x=>x.Id == input.Id);
			task.Title = input.Title;
			task.Description = input.Description;
			await _taskRepository.UpdateAsync(task);
		}


		//Xóa - D
		public async Task Delete(int Id)
		{
			await _taskRepository.DeleteAsync(Id);
		}

		//Đọc 1 - R
		public async Task<TaskListDto> GetTask(int id)
		{
			var result = await _taskRepository.GetAsync(id);
			TaskListDto taskListDto = new TaskListDto();
			taskListDto.Id = result.Id;
			taskListDto.Title = result.Title;
			taskListDto.Description = result.Description;
			taskListDto.State = result.State;
			taskListDto.CreationTime = result.CreationTime;
			return taskListDto;

		}



	}
}