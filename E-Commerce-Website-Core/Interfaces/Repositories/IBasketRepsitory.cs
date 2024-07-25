using E_Commerce_Website_Core.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Repositories
{
	public interface IBasketRepsitory
	{
		Task<bool> DeleteBasketAsync(string id);
		Task<CustomerBasket?> GetBasketAsync(string id);
		Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket customerBasket);
	}
}
