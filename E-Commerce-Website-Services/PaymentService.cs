using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Services;
using Product =  E_Commerce_Website_Core.Models.Product;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Website_Core.Models.Order_Entities;
using E_Commerce_Website_Repository.Specification;

namespace E_Commerce_Website_Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IMapper _mapper;
		private readonly IConfiguration _configure;
		private readonly IBasketService _basketService;
		private readonly IUnitOfWork _unitfWork;

		public PaymentService(IMapper mapper, IConfiguration configure, IBasketService basketService, IUnitOfWork unitfWork)
		{
			_mapper = mapper;
			_configure = configure;
			_basketService = basketService;
			_unitfWork = unitfWork;
		}

		public async Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket)
		{
			StripeConfiguration.ApiKey = _configure["Stripe:SecretKey"];
            foreach (var item in basket.itemBaskets)
            {
                var product = await _unitfWork.Repositry<Product,int>().GetAsync(item.ProductId);
				if(product.Price != item.Price)
					item.Price = product.Price;
            }
			var Total = basket.itemBaskets.Sum(item => item.Price * item.Quantity);

			if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Payment Method Selected");
			var deliveryMethod = await _unitfWork.Repositry<DeliveryMethod , int>().GetAsync(basket.DeliveryMethodId.Value);
			var ShippingPrice = deliveryMethod.Price;
			long amount = (long) (ShippingPrice + Total);
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = "Usd",
					PaymentMethodTypes = new List<string> { "Card" },					
				};
				paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.ClientSecret;
			}
			else
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};
				await service.UpdateAsync(basket.PaymentIntentId, options);
			}
			await _basketService.UpdateBasketAsync(basket);
			return basket;
		}

		public async Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrders(string basketId)
		{
			StripeConfiguration.ApiKey = _configure["Stripe:SecretKey"];
			var basket = await _basketService.GetBasketAsync(basketId);
			if (basket is null) throw new Exception($"No Basket Wiht Id {basketId}");
			foreach (var item in basket.itemBaskets)
			{
				var product = await _unitfWork.Repositry<Product, int>().GetAsync(item.ProductId);
				if (product.Price != item.Price)
					item.Price = product.Price;
			}
			var Total = basket.itemBaskets.Sum(item => item.Price * item.Quantity);

			if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Payment Method Selected");
			var deliveryMethod = await _unitfWork.Repositry<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
			var ShippingPrice = deliveryMethod.Price;
			long amount = (long)(ShippingPrice*100 + Total*100);
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = "Usd",
					PaymentMethodTypes = new List<string> { "Card" },
				};
				paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.ClientSecret;
			}
			else
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};
				await service.UpdateAsync(basket.PaymentIntentId, options);
			}
			await _basketService.UpdateBasketAsync(basket);
			return basket;
		}

		public async Task<OrderResultDto> UpdatePaymentStatusFailed(string intentPaymentId)
		{
			var spec = new OrderWithPaymentIntentIdSpecification(intentPaymentId);
			var order = await _unitfWork.Repositry<Order , Guid>().GetWithSpecAsync(spec);
			if (order is null)
				throw new Exception($"No Order With This Paynment Intent Id {intentPaymentId}");
			order.PaymentStatus = PaymentStatus.Failed;
			_unitfWork.Repositry<Order, Guid>().Update(order);
			return _mapper.Map<OrderResultDto>(order);
		}

		public async Task<OrderResultDto> UpdatePaymentStatusSucceeded(string intentPaymentId)
		{
			var spec = new OrderWithPaymentIntentIdSpecification(intentPaymentId);
			var order = await _unitfWork.Repositry<Order, Guid>().GetWithSpecAsync(spec);
			if (order is null)
				throw new Exception($"No Order With This Paynment Intent Id {intentPaymentId}");
			order.PaymentStatus = PaymentStatus.Received;
			_unitfWork.Repositry<Order, Guid>().Update(order);
			await _basketService.DeleteBasketAsync(order.BasketId);
			return _mapper.Map<OrderResultDto>(order);
		}
	}
	
}
