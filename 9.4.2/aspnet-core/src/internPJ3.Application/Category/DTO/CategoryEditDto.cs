using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using internPJ3.Category;


namespace internPJ3.Category.DTO
{
	
	public class CategoryEditDto : Entity<int>
	{
		public int ProductCount { get; set; }
		public string CategoryName { get; set; }
		public string? CategoryDescription { get; set; }
		//public DateTime CreationTime { get; set; }

	}
}
