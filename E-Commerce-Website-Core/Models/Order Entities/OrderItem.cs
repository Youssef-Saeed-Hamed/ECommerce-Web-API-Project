using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Models.Order_Entities
{
	public class OrderItem : BaseEntity<Guid>
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public OrderItemProduct Product { get; set; }
	}
}
