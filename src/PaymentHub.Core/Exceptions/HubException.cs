using System;
using System.Collections.Generic;
using System.Net;

namespace PaymentHub.Core.Exceptions
{
	public class HubException : Exception
	{
		public Exception InnerException { get; }
		public readonly HttpStatusCode StatusCode;
		public readonly List<Error> Errors;

		protected HubException()
		{
			this.Errors = new List<Error>();
		}

		public HubException(HttpStatusCode statusCode, Error error, Exception innerException) : this()
		{
			InnerException = innerException;
			this.Errors.Add(error);
		}

		public HubException(HttpStatusCode statusCode, List<Error> errors)
		{
			StatusCode = statusCode;
			Errors = errors;
		}

	}

	public class Error
	{
		public Error(string message)
		{
			Message = message;
		}

		public string Message { get; private set; }
	}
}