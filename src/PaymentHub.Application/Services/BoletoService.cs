using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using PaymentHub.Application.DTOs;
using PaymentHub.Application.Interfaces;
using PaymentHub.Application.Services.Transactions.Command;
using PaymentHub.Core.Const;
using PaymentHub.Core.Enum;
using PaymentHub.Core.Exceptions;
using PaymentHub.Domain;

namespace PaymentHub.Application.Services
{
	public class BoletoService : IBoletoService
	{
		private readonly IBoletoGatewayFactory _boletoGatewayFactory;
		private readonly IPaymentHubContext _context;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		private BoletoService(IBoletoGatewayFactory boletoGatewayFactory,
			IPaymentHubContext context,
			IMediator mediator,
			IMapper mapper)
		{
			_boletoGatewayFactory = boletoGatewayFactory;
			_context = context;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task<TransactionResponse> CreateBoletoAsync(BoletoRequest request, Guid companyId)
		{
			var company = await _context.Company.Find(x => x.Id == companyId).FirstOrDefaultAsync();

			var gateway = GetGatewayBoleto(company);

			var service = _boletoGatewayFactory.CreateBoletoGateway(gateway);

			var boleto = await service.CreateBoletoAsync(request);

			var transaction = new Transaction();

			await _mediator.Send(new CreateTransactionCommand(transaction));

			return _mapper.Map<Transaction, TransactionResponse>(transaction);
		}

		private EnumGatewayBoleto GetGatewayBoleto(Company company)
		{
			var configuration =
				company.Configurations.FirstOrDefault(x => x.Key == ConfigurationKey.BoletoGatewayReference);

			if (string.IsNullOrWhiteSpace(configuration?.Value))
				throw HubExceptionFactory.Create(HttpStatusCode.InternalServerError,
												 $"Configuration invalid for {ConfigurationKey.BoletoGatewayReference}");

			return MapGatewayBoletoConfigurationToEnum(configuration);
		}

		private EnumGatewayBoleto MapGatewayBoletoConfigurationToEnum(Configuration configuration)
		{
			if (!int.TryParse(configuration.Value, out var result))
				throw HubExceptionFactory.Create(HttpStatusCode.InternalServerError, $"Invalid configuration");

			if (!Enum.IsDefined(typeof(EnumGatewayBoleto), result))
				throw HubExceptionFactory.Create(HttpStatusCode.InternalServerError, $"Invalid Configuration");

			return (EnumGatewayBoleto)result;
		}
	}
}