using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using internPJ3.Person1;

namespace internPJ3.Tasks1
{
    [Table("AppTasks")] //Xác định bảng trong dtb để lưu dữ liệu của Enity
    public class TaskTest : Entity, IHasCreationTime
    {
        public const int MaxTitleLength = 256;             //Khai báo hằng số
        public const int MaxDescriptionLength = 64 * 1024; //cho các thuộc tính

        [Required] // Bắt buộc có, không được phép null
        [StringLength(MaxTitleLength)]  // Độ dài tối đa chuỗi = MaxTitleLength = 256
        public string Title { get; set; }

        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public TaskTest()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }


        public TaskTest(string title, string description = null
          , int? assignedPersonId = null
          )

            : this()
        {
            Title = title;
            Description = description;
            AssignedPersonId = assignedPersonId;
    }

    [ForeignKey(nameof(AssignedPersonId))]
    public Person AssignedPerson { get; set; }
    public int? AssignedPersonId { get; set; }
  }

    public enum TaskState : byte
    {
        Open = 0,
        Completed = 1
    }
}
