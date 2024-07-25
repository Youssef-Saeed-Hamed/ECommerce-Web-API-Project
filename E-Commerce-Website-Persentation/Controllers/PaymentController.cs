using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce_Website_Persentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly IConfiguration _configuration;
		const string endpointSecret = "whsec_275972d9369a031bbc45e0719d5466894aa176f76f01bce4301ae0a37b59e8ce";

		public PaymentController(IPaymentService paymentService, IConfiguration configuration)
		{
			_paymentService = paymentService;
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<ActionResult<BasketDto>> CreatePaymentIntent(BasketDto input)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input);
			return Ok(basket);
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> Index()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], endpointSecret);

				// Handle the event
				if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
				{
				}
				else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
				{
				}
				// ... handle other event types
				else
				{
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}

				return Ok();
			}
			catch (StripeException e)
			{
				return BadRequest();
			}
		}
	}
}
