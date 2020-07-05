using PaymentHub.Core.Enum;

namespace PaymentHub.Application.Interfaces
{
	public interface IBoletoGatewayFactory
	{
		IBoletoGateway CreateBoletoGateway(EnumGatewayBoleto reference);
	}
}