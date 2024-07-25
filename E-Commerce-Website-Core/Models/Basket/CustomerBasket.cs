using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Models.Basket
{
	public class CustomerBasket
	{
		public string Id { get; set; }
		public int DeliveryMethodId { get; set; }
		public decimal Price { get; set; }
		public List<BasketItem> itemBaskets { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? BasketId { get; set; }
	}
}
