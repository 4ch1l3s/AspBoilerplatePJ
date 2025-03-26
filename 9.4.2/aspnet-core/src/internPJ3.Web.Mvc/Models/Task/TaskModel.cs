using internPJ3.Products.DTO;
using internPJ3.Roles.Dto;
using internPJ3.Tasks.DTO;
using internPJ3.Tasks1;
using System;
using System.Collections.Generic;

namespace internPJ3.Web.Models.Task
{
	public class TaskModel
    {
        public IReadOnlyList<TaskListDto> TaskListDtos { get; set; }
        // IreadOnlyList => Interface trong C# đại diện cho 1 danh sách chỉ đọc
        // TaskListDto => kiểu dữ liệu của phần tử trong danh sách
        // Tên thuộc tính, được đặt theo kiểu dữ liệu của phần tử trong dsach
        // get - set => Cú pháp auto implement prop trong C# (get : đọc, set : update)

    }
}
