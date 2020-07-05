using System.Collections.Generic;
using PaymentHub.Core.Domain;

namespace PaymentHub.Domain
{
	public class Company : Entity
	{
		public string Name { get; set; }

		public string AppKey { get; set; }

		public ICollection<Configuration> Configurations { get; set; }
	}

	public class Configuration
	{
		public string Key { get; set; }

		public string Value { get; set; }
	}
}