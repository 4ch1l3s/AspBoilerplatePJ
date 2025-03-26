using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Abp.AutoMapper;
using internPJ3.Tasks1;

namespace internPJ3.Tasks.DTO
{
	[AutoMapTo(typeof(TaskTest))]
	public class CreateTaskInput
	{
		[Required]
		[StringLength(TaskTest.MaxTitleLength)]
		public string Title { get; set; }

		[StringLength(TaskTest.MaxDescriptionLength)]
		public string Description { get; set; }
		public TaskState State { get; set; }

		public int? AssignedPersonId { get; set; }
	}
}