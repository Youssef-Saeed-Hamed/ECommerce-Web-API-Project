using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Models.Order_Entities;

namespace E_Commerce_Website_Persentation.Helper
{
	public class OrderProfile : Profile
	{
		public OrderProfile() 
		{ 
			CreateMap<ShippingAddress,  AddressDto>().ReverseMap();

			CreateMap<Order, OrderResultDto>()
				.ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(d => d.ShippingPrice , opt => opt.MapFrom(s => s.DeliveryMethod.Price));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductID, opt => opt.MapFrom(s => s.Product.ProductID))
				.ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
				.ForMember(d => d.PictureUrl, opt => opt.MapFrom(s => s.Product.PictureUrl))
				.ForMember(d => d.PictureUrl, opt => opt.MapFrom<OrderItemResolver>());
		}
	}
}
