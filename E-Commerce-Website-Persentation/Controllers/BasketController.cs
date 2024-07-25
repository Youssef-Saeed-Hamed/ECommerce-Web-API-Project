using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Persentation.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Website_Persentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketService _basketService;

		public BasketController(IBasketService basketService)
		{
			_basketService = basketService;
		}


		[HttpGet]
		public async Task<ActionResult<BasketDto>> Get(string Id)
		{
			var basket = await _basketService.GetBasketAsync(Id);
			return basket is null ? NotFound(new APIResponse(404, $"The Basket With Id {Id} Is Not Found")) : Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<BasketDto>> update(BasketDto basketDto)
		{
			var basket = await _basketService.UpdateBasketAsync(basketDto);
			return basket is null ? NotFound(new APIResponse(404, $"The Basket With Id {basketDto.Id} Is Not Found")) : Ok(basket);
		}

		[HttpDelete]
		public async Task<ActionResult> delete(string Id)
			=> Ok(await _basketService.DeleteBasketAsync(Id));

	}
}
