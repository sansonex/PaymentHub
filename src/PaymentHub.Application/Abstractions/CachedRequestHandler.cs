using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace PaymentHub.Application.Abstractions
{
	public abstract class CachedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		protected readonly IMemoryCache Cache;

		protected CachedRequestHandler(IMemoryCache cache)
		{
			Cache = cache;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			await TryGetCachedValue(request, out var response);
			if (response != null)
				return response;

			response = await GetValue(request, cancellationToken);

			if (response != null)
				await SetCacheValue(request, response);

			return response;
		}


		protected abstract Task<TResponse> GetValue(TRequest request, CancellationToken cancellationToken);

		protected abstract Task TryGetCachedValue(TRequest request, out TResponse response);

		protected abstract Task SetCacheValue(TRequest request, TResponse response);
	}
}