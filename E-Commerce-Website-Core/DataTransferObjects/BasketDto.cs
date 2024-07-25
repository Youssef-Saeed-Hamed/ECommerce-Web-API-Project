using E_Commerce_Website_Core.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.DataTransferObjects
{
	public class BasketDto
	{
		public string Id { get; set; }
		public int? DeliveryMethodId { get; set; }
		public decimal Price { get; set; }
		public List<BasketItemDto> itemBaskets { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? BasketId { get; set; }
	}
}
