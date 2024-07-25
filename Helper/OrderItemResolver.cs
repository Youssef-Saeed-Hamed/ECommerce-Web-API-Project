using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Models.Order_Entities;

namespace E_Commerce_Website_Persentation.Helper
{
	internal class OrderItemResolver  : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderItemResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		 => !string.IsNullOrWhiteSpace(source.Product.PictureUrl) ?
			$"{_configuration["BaseUrl"]}{source.Product.PictureUrl}" :
			string.Empty;


	}

}