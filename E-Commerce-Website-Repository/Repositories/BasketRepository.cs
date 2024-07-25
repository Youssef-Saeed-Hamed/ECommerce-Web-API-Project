using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Models.Basket;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace E_Commerce_Website_Repository.Repositories
{
	public class BasketRepository : IBasketRepsitory
	{
		private readonly IDatabase _database ;
		public BasketRepository(IConnectionMultiplexer connection)
		{
			_database = connection.GetDatabase(); 
		}

		public async Task<bool> DeleteBasketAsync(string id)	
			=>  await _database.KeyDeleteAsync(id);
		

		public async Task<CustomerBasket?> GetBasketAsync(string id)
		{
			var basket = await _database.StringGetAsync(id);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket customerBasket)
		{
			var SerilizarBasket = JsonSerializer.Serialize<CustomerBasket>(customerBasket);
			var result = await _database.StringSetAsync(customerBasket.Id, SerilizarBasket, TimeSpan.FromDays(7));
			return result ? await GetBasketAsync(customerBasket.Id) : null;

		}
	}
}
