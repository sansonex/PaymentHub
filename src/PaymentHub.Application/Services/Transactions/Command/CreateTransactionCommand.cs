using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaymentHub.Application.Interfaces;
using PaymentHub.Domain;

namespace PaymentHub.Application.Services.Transactions.Command
{
	public class CreateTransactionCommand : IRequest
	{
		public Transaction Transaction { get; }

		public CreateTransactionCommand(Transaction transaction)
		{
			Transaction = transaction;
		}
	}

	public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand>
	{
		private readonly IPaymentHubContext _context;

		public CreateTransactionCommandHandler(IPaymentHubContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
		{
			await _context.Transaction.InsertOneAsync((request.Transaction), cancellationToken: cancellationToken);

			return await Task.FromResult(Unit.Value).ConfigureAwait(false);
		}
	}
}