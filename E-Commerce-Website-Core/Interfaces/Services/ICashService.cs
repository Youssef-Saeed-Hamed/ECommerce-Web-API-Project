using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Services
{
	public interface ICashService
	{
		public Task SetCashResponseAsync(string Key, object response, TimeSpan time);
		public Task<string?> GetCashResponseAsync(string key);
	}
}
