using E_Commerce_Website_Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce_Website_Persentation.Helper
{
	public class CashAttribute : Attribute, IAsyncActionFilter
	{
		private readonly  int _time;

		public CashAttribute(int time)
		{
			_time = time;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var CashKey = GenerateKeyFromRequest(context.HttpContext.Request);
			var CashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

			var CashResponse = await CashService.GetCashResponseAsync(CashKey);
			if (CashResponse is not null) 
			{
				var result = new ContentResult
				{
					ContentType = "application/json",
					StatusCode = 200,
					Content = CashResponse
				};
				context.Result = result;
				return;
			}
			var executedContext = await next();
			if(executedContext.Result is OkObjectResult response)
			{
				await CashService.SetCashResponseAsync(CashKey , response.Value , TimeSpan.FromSeconds(_time));
			}
		}

		private string GenerateKeyFromRequest(HttpRequest request)
		{
			StringBuilder key = new StringBuilder();
			key.Append(request.Path);

            foreach (var item in request.Query.OrderBy(x => x.Value))            
				key.Append($"{item}");

			return key.ToString();
        }
	}
}
