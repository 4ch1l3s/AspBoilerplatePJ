using internPJ3.Products.DTO;
using internPJ3.Roles.Dto;
using internPJ3.Product;
using internPJ3.Tasks1;
using System;
using System.Collections.Generic;
using internPJ3.Category.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using internPJ3.Tour.DTO;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace internPJ3.Web.Models.Tour
{
	public class TourModel : FullAuditedEntity<long>
	{
		public IReadOnlyList<TourListDto> TourListDtos { get; set; } //list
		// IreadOnlyList => Interface trong C# đại diện cho 1 danh sách chỉ đọc
		// ProductListDto => kiểu dữ liệu của phần tử trong danh sách
		// Tên thuộc tính, được đặt theo kiểu dữ liệu của phần tử trong dsach
		// get - set => Cú pháp auto implement prop trong C# (get : đọc, set : update)
		//public List<SelectList> selectListItems { get; set; }
		
    public string TourName { get; set; }
    public int MinGroupSize { get; set; }
    public int MaxGroupSize { get; set; }
    public long TourTypeCid { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Transportation { get; set; }
    public decimal? TourPrice { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string Attachment { get; set; }
	
    public DateTime CreationTime { get; set; }
    public long? CreatorUserId { get; set; }
		public DateTime LastModificationTime {get; set; }
		public long? LastModifierUserId { get; set; }
		public bool IsDeleted { get; set; }
		public long? DeleterUserId { get; set; }
		public DateTime DeletionTime {get; set; }

	}
}

