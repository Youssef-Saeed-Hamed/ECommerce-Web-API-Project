using E_Commerce_Website_Core.Models.Order_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Specification
{
	public class OrderSpecification : BaseSpecification<Order>
	{
		public OrderSpecification(string email) : 
			base(o => o.ByerEmail == email)
		{
			Includes.Add(order => order.DeliveryMethod);
			Includes.Add(order => order.orderItems);
			OrderDesc = o => o.OrderDate;
		}
		public OrderSpecification(Guid id , string email) : 
			base(o => o.ByerEmail == email && o.Id == id)
		{
			Includes.Add(order => order.DeliveryMethod);
			Includes.Add(order => order.orderItems);
		}


	}
}
