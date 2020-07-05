using System;

namespace PaymentHub.Core.Domain
{
	public abstract class Entity
	{
		public Guid Id { get; set; }
	}
}