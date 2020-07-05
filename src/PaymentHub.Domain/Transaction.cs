using System;
using PaymentHub.Core.Domain;

namespace PaymentHub.Domain
{
	public class Transaction : Entity
	{
		public Transaction()
		{
			this.Id = Guid.NewGuid();
		}
	}
}