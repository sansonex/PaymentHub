using System.Net;

namespace PaymentHub.Core.Exceptions
{
	public static class HubExceptionFactory
	{
		public static HubException Create(HttpStatusCode statusCode, string message)
		{
			return new HubException(statusCode, new Error(message), null);
		}
	}
}