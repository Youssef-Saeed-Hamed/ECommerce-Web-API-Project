using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.DataTransferObjects
{
	public class OrderDto
	{
		public string BasketId { get; set; }
		public string ByerEmail {  get; set; }
		public int ? DeliveryMethodId { get; set; }
		public AddressDto ShippingAddress {  get; set; }
	}
}
