using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;


namespace internPJ3.Cart
{
 
	public class CartE : Entity<int>

	{
		public int UserId { get; set; }
		public long CartStatus { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public int Coupon { get; set; }
		public DateTime CreationTime { get; set; }


	}

}
