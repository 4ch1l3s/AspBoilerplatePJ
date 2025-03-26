using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using internPJ3.Tour;

namespace internPJ3.Tour
{
	[Table("AppTours")]
	public class TourE : FullAuditedEntity<long>
	{
		[Required]
		public string TourName { get; set; } // Tên tour

		public int MinGroupSize { get; set; } // Số khách tối thiểu

		public int MaxGroupSize { get; set; } // Số khách tối đa

		public long TourTypeCid { get; set; } // Kiểu tour: Tour du lịch nội tỉnh, Tour du lịch liên tỉnh, Tour du lịch quốc tế // select 

		public DateTime? StartDate { get; set; } // thời gian bắt đầu

		public DateTime? EndDate { get; set; } // thời gian kết thúc

		[Required]
		public string Transportation { get; set; } // Phương tiện

		public decimal? TourPrice { get; set; } // Giá tour

		[Required]
		public string PhoneNumber { get; set; } // Điện thoại

		[Required]
		public string Description { get; set; } // Mô tả

		public string Attachment { get; set; } // Đính kèm

		public TourE()
		{

		}



	}
}

