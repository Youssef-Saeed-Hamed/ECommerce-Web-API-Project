using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models.Order_Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_Website_Persentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]

		public async Task<ActionResult<OrderResultDto>> Create(OrderDto input)
		{
			var order = await _orderService.CreateOrderAsync(input);
			return Ok(order);
		}
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrders()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var orders = await _orderService.GetAllOrderAsync(email);
			return Ok(orders);
		}
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResultDto>> GetOrder(Guid id)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrderAsync(id,email);
			return Ok(order);
		}
		[HttpGet("{delivery}")]
		public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
		{
			return Ok(await  _orderService.GetDeliveryMethodsAsync());
		}
	}
}
