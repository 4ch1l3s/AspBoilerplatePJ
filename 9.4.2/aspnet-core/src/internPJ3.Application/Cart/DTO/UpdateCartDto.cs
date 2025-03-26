using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using internPJ3.Product;
using Microsoft.AspNetCore.Http;

namespace internPJ3.Cart.DTO
{
	public class UpdateCartDto : EntityDto
	{
		public long  CartStatus  { get ; set;}
		public string Address { get; set;}
		public string PhoneNumber { get; set;}
		public int Coupon {  get; set;}


	}
}
