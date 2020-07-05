using System;
using Microsoft.Extensions.DependencyInjection;
using PaymentHub.Application.Interfaces;
using PaymentHub.Core.Enum;
using PaymentHub.Infrastructure.Gateways;

namespace PaymentHub.Infrastructure
{
	public static class DependecyInjection
	{
		public static IServiceCollection AddInfrastructure(IServiceCollection services)
		{
			services.AddScoped<IBoletoGatewayFactory, IBoletoGatewayFactory>();
			services.AddScoped<PagSeguroGateway>();

			services.AddScoped<Func<EnumGatewayBoleto, IBoletoGateway>>(service => key =>
			{
				return key switch
				{
					EnumGatewayBoleto.PagSeguro => service.GetService<PagSeguroGateway>(),
					_ => throw new Exception($"Missing configuration for service of reference {key}")
				};
			});

			return services;
		}
	}
}