using E_Commerce_Website_Core.Models.Order_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Specification
{
	public class OrderWithPaymentIntentIdSpecification : BaseSpecification<Order>
	{
        public OrderWithPaymentIntentIdSpecification(string paymentIntentId) : base(order => order.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
