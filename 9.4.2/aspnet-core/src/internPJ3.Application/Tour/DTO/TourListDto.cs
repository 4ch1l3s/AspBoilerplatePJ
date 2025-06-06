﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using internPJ3.Category;
using internPJ3.Tasks1;


namespace internPJ3.Tour.DTO
{
	public class TourListDto : EntityDto
	{
		public long Id { get; set; }

		public string TourName { get; set; } // Tên tour

		public int MinGroupSize { get; set; } // Số khách tối thiểu

		public int MaxGroupSize { get; set; } // Số khách tối đa

		public long TourTypeCid { get; set; } // Kiểu tour: Tour du lịch nội tỉnh, Tour du lịch liên tỉnh, Tour du lịch quốc tế // select 

		public DateTime? StartDate { get; set; } // thời gian bắt đầu

		public DateTime? EndDate { get; set; } // thời gian kết thúc

		public string Transportation { get; set; } // Phương tiện

		public decimal? TourPrice { get; set; } // Giá tour

	
		public string PhoneNumber { get; set; } // Điện thoại

		
		public string Description { get; set; } // Mô tả

		public string Attachment { get; set; } // Đính kèm

		public string GroupSize { get; set; } //String

		public string DateT { get; set; } //String
	}

}
