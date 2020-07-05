using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentHub.Application.DTOs;
using PaymentHub.Application.Interfaces;
using PaymentHub.Domain;
using Uol.PagSeguro.Exceptions;
using Uol.PagSeguro.Models;
using IBoletoService = Uol.PagSeguro.Interfaces.IBoletoService;

namespace PaymentHub.Infrastructure.Gateways
{
	public class PagSeguroGateway : IBoletoGateway
	{
		private readonly IBoletoService _boletoService;
		private readonly ILogger _log;

		public PagSeguroGateway(IBoletoService boletoService, ILoggerFactory loggerFactory)
		{
			_boletoService = boletoService;
			_log = loggerFactory.CreateLogger(nameof(PagSeguroGateway));
		}

		public async Task<Boleto> CreateBoletoAsync(BoletoRequest request)
		{
			var boletoRequest = new PagSeguroBoletoRequest();

			try
			{
				var boleto = await _boletoService.CreateBoletoAsync(boletoRequest);
				// TODO map to boleto
				return new Boleto();
			}
			catch (PagSeguroException e)
			{
				_log.LogError($"Error creating boleto on {nameof(PagSeguroGateway)} | {e.Errors}");
				// TODO map to domain exception
				throw;
			}
		}

	}
}