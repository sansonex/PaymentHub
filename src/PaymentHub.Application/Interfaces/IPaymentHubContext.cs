using PaymentHub.Domain;
using MongoDB;
using MongoDB.Driver;

namespace PaymentHub.Application.Interfaces
{
	public interface IPaymentHubContext
	{
		IMongoCollection<Company> Company { get; }

		IMongoCollection<Transaction> Transaction { get; }
	}
}