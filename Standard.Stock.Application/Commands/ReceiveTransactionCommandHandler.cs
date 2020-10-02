using MediatR;
using Standard.Framework.Result.Abstraction;
using Standard.Framework.Result.Concrete;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Domain.Enuns;
using Standard.Stock.Infrastructure.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Stock.Application.Commands
{
    public class ReceiveTransactionCommandHandler : IRequestHandler<ReceiveTransactionCommand, IApplicationResult<string>>, IDisposable
    {
        private bool disposed = false;
        private ITransactionRepository TransactionRepository { get; }
        private StockContext Context { get; }

        public ReceiveTransactionCommandHandler(ITransactionRepository transactionRepository, StockContext context) 
        {
            TransactionRepository = transactionRepository;
            Context = context;
        }

        public async Task<IApplicationResult<string>> Handle(ReceiveTransactionCommand request, CancellationToken cancellationToken)
        {
            IApplicationResult<string> result = new ApplicationResult<string>();
            TransactionType type = request.Type == TransactionType.Buy ? TransactionType.Sell : TransactionType.Buy;

            Transaction transaction = new Transaction(request.Initials,
                                                      request.Type,
                                                      request.Price,
                                                      request.Quantity);
            TransactionRepository.Insert(transaction);
            
            await Context.SaveChangesAsync();
            result.Result = "Transaction received";

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                TransactionRepository.Dispose();

            disposed = true;
            GC.Collect();
        }
    }
}
