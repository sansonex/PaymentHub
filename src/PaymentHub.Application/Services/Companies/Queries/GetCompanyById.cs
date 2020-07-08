using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using PaymentHub.Application.Abstractions;
using PaymentHub.Application.Interfaces;
using PaymentHub.Domain;

namespace PaymentHub.Application.Services.Companies.Queries
{
	public class GetCompanyById : IRequest<Company>
	{
		public Guid Id { get; }

		public GetCompanyById(Guid id)
		{
			Id = id;
		}

	}

	public class GetCompanyByIdHandler : CachedRequestHandler<GetCompanyById, Company>
	{
		private readonly IPaymentHubContext _context;

		public GetCompanyByIdHandler(IPaymentHubContext context, IMemoryCache cache) : base(cache)
		{
			_context = context;
		}

		protected override Task<Company> GetValue(GetCompanyById request, CancellationToken cancellationToken)
		{
			return _context.Company.Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
		}

		protected override Task TryGetCachedValue(GetCompanyById request, out Company response)
		{
			response = Cache.TryGetValue($"{request.GetType().FullName}:{request.Id.ToString()}", out Company result) ? result : null;

			return Task.CompletedTask;
		}

		protected override Task SetCacheValue(GetCompanyById request, Company response)
		{
			Cache.Set($"{request.GetType().FullName}:{response.Id.ToString()}", response, TimeSpan.FromHours(1));
			return Task.CompletedTask;
		}
	}
}