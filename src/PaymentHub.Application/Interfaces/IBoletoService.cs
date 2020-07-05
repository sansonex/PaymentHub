using System;
using System.Threading.Tasks;
using PaymentHub.Application.DTOs;

namespace PaymentHub.Application.Interfaces
{
	public interface IBoletoService
	{
		Task<TransactionResponse> CreateBoletoAsync(BoletoRequest request, Guid companyId);
	}
}