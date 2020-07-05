using System.Threading.Tasks;
using PaymentHub.Application.DTOs;
using PaymentHub.Domain;

namespace PaymentHub.Application.Interfaces
{
	public interface IBoletoGateway
	{
		Task<Boleto> CreateBoletoAsync(BoletoRequest request);
	}
}