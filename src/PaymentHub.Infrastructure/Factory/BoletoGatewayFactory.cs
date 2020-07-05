using System;
using PaymentHub.Application.Interfaces;
using PaymentHub.Core.Enum;

namespace PaymentHub.Infrastructure.Factory
{
	public class BoletoGatewayFactory : IBoletoGatewayFactory
	{
		private readonly Func<EnumGatewayBoleto, IBoletoGateway> _serviceProvider;

		public BoletoGatewayFactory(Func<EnumGatewayBoleto, IBoletoGateway> serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IBoletoGateway CreateBoletoGateway(EnumGatewayBoleto reference)
		{
			return _serviceProvider(reference);
		}
	}
}