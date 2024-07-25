using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Models.Order_Entities;
using E_Commerce_Website_Repository.Specification;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Services
{
	public class OrderService : IOrderService
	{
		private readonly IBasketService _basketService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPaymentService _paymentService;


		public OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
		{
			_basketService = basketService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_paymentService = paymentService;
		}

		public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
		{
			var basket = await _basketService.GetBasketAsync(input.BasketId);
			if (basket is null) throw new Exception($"No Basket With Id {input.BasketId} Was Found");
			
			var OrderItems = new List<OrderItemDto>();
            foreach (var item in basket.itemBaskets)
            {
				var product = await _unitOfWork.Repositry<Product, int>().GetAsync(item.ProductId);
				if (product is null) continue;
				var productItem = new OrderItemProduct
				{
					PictureUrl = product.PictureUrl,
					ProductID = product.Id,
					ProductName = product.Name,
				};
				var orderItem = new OrderItem
				{
					Product = productItem,
					Quantity = item.Quantity,
					Price = product.Price
				};

				var mappedItem = _mapper.Map<OrderItemDto>(orderItem);
				OrderItems.Add(mappedItem);
                
            }
			if (OrderItems.Any()) throw new Exception("No Basket Items Was Found");

			if (!input.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Was Selected");

			var delivery = await _unitOfWork.Repositry<DeliveryMethod, int>().GetAsync(input.DeliveryMethodId.Value);

			var shippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
			var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);
			var spec = new OrderWithPaymentIntentIdSpecification(basket.PaymentIntentId);
			var existingOrder = await _unitOfWork.Repositry<Order, Guid>().GetWithSpecAsync(spec);
            if (existingOrder is not null)
            {
                _unitOfWork.Repositry<Order ,Guid >().Delete(existingOrder);
				await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);
            }
            else
            {
                 basket  = await _paymentService.CreateOrUpdatePaymentIntentForNewOrders(basket.Id);
			;}

            var mappedItems = _mapper.Map<List<OrderItem>>(OrderItems); 

			if (delivery is null ) throw new Exception("No Delivery Method  Was Found");
			var order = new Order
			{
				ByerEmail = input.ByerEmail,
				ShippingAddress = shippingAddress,
				DeliveryMethod = delivery,
				orderItems = mappedItems,
				SubTotal = subTotal,
				PaymentIntentId = basket.PaymentIntentId,
				BasketId = basket.Id
			};
			await _unitOfWork.Repositry<Order , Guid>().AddAsync(order);
			return _mapper.Map<OrderResultDto>(order);
			

        } 

		public async Task<IEnumerable<OrderResultDto>> GetAllOrderAsync(string email)
		{

			var spec = new OrderSpecification(email);
			var orders = await _unitOfWork.Repositry<Order , Guid>().GetAllWithSpecAsync(spec);
			if (!orders.Any()) throw new Exception($"No Orders With This Email {email}");
			return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
		} 

		public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
			=> await _unitOfWork.Repositry<DeliveryMethod , int>().GetAllAsync();

		public async Task<OrderResultDto> GetOrderAsync(Guid id, string email)
		{
			var spec = new OrderSpecification(id , email);
			var order = await _unitOfWork.Repositry<Order, Guid>().GetWithSpecAsync(spec);
			if (order is null) throw new Exception($"No Order With Id {id}");
			return _mapper.Map<OrderResultDto>(order);
		}
	}
}
