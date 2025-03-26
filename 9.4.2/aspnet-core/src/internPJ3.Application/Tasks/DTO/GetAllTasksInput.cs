using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internPJ3.Tasks1;

namespace internPJ3.Tasks.DTO
{
    public class GetAllTasksInput//TIM KIEM
    {
		public TaskState? State { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
	}
}
