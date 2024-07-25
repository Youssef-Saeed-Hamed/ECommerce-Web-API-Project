using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Services
{
	public class BasketService : IBasketService
	{
		private readonly IBasketRepsitory _repository;
		private readonly IMapper _mapper;

		public BasketService(IBasketRepsitory repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<bool> DeleteBasketAsync(string id)
			=> await _repository.DeleteBasketAsync(id);

		public async Task<BasketDto?> GetBasketAsync(string id)
		{
			var basket = await _repository.GetBasketAsync(id);
			var basketMapper = _mapper.Map<BasketDto>(basket);
			return basket is null ? null : basketMapper;
		}

		public async Task<BasketDto?> UpdateBasketAsync(BasketDto? basket)
		{
			var customerBasket = _mapper.Map<CustomerBasket>(basket);
			var UpdatedBasket = await _repository.UpdateCustomerBasketAsync(customerBasket);
			var basketDto  = _mapper.Map<BasketDto?>(UpdatedBasket);
			return basketDto is null ? null : basketDto;
			 
		}
	}
}
