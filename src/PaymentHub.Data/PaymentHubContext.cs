using MongoDB.Driver;
using PaymentHub.Application.Interfaces;
using PaymentHub.Domain;

namespace PaymentHub.Data
{
	public class PaymentHubContext : IPaymentHubContext
	{
		private readonly MongoClient _client;
		private readonly IMongoDatabase _database;

		public PaymentHubContext(MongoClient client, string database)
		{
			_client = client;
			_database = _client.GetDatabase(database);
		}

		public IMongoCollection<Company> Company => _database.GetCollection<Company>(nameof(Company));

		public IMongoCollection<Transaction> Transaction => _database.GetCollection<Transaction>(nameof(Transaction));
	}
}