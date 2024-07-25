using E_Commerce_Website_Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Services
{
	public interface IPaymentService
	{
		public Task<BasketDto>CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket);
		public Task<BasketDto>CreateOrUpdatePaymentIntentForNewOrders(string basketId);
		public Task<OrderResultDto> UpdatePaymentStatusFailed(string intentPaymentId);
		public Task<OrderResultDto> UpdatePaymentStatusSucceeded(string intentPaymentId);
	}

}
