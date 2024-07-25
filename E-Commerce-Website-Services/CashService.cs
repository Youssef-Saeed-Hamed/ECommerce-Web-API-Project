using E_Commerce_Website_Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace E_Commerce_Website_Services
{
	public class CashService : ICashService
	{
		private readonly IDatabase _database;
		public CashService(IConnectionMultiplexer connection)
		{
			_database = connection.GetDatabase();
		}
		public async Task<string?> GetCashResponseAsync(string key)
		{
			var response = await _database.StringGetAsync(key);
			return response.IsNullOrEmpty ? null : response.ToString();
		}


		public async Task SetCashResponseAsync(string Key, object resppnse, TimeSpan time)
		{
			var serializerResponse = JsonSerializer.Serialize(resppnse,
				new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

		 	await _database.StringSetAsync(Key, serializerResponse, time);
		}
	}
}
