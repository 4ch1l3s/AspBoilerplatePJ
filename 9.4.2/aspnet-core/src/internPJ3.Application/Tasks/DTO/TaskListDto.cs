using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using internPJ3.Tasks1;

namespace internPJ3.Tasks.DTO
{

	public class TaskListDto : EntityDto, IHasCreationTime 
	{

		//public int id { get; set; } ~= EntityDto
		//public datetime CreationTime { get; set; } ~= IHasCreationTime

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime CreationTime { get; set; }

		public TaskState State { get; set; }

		public int? AssignedPersonId { get; set; }

		public string AssignedPersonName { get; set; }
	}
}
