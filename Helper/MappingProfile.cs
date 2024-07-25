using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Models.Basket;

namespace E_Commerce_Website_Persentation.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<ProductBrand , BrandAndTypesToReturnDto>();
            CreateMap<ProductType, BrandAndTypesToReturnDto>();
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());
                
            CreateMap<CustomerBasket , BasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>().ReverseMap();
        }
    }
}
