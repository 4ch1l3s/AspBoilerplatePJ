using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;


namespace internPJ3.Category
{
	[Table("Category")]
	public class Categories : Entity<int>

	{
		[Required]
		public string CategoryName { get; set; }
		//public DateTime CreationTime { get; set; }
		public string? CategoryDescription { get; set; }
		public Categories() { }

		public Categories(string categoryName)
		{
			CategoryName = categoryName;
		}
	}

}
