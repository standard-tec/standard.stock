using MediatR;
using Standard.Framework.Result.Abstraction;
using Standard.Framework.Result.Concrete;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Domain.Enuns;
using Standard.Stock.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
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

            List<Transaction> transactions = new List<Transaction>(); /* await TransactionRepository.Get(it => it.Initials == request.Initials,
                                                                             it => it.IsComplete == false,
                                                                             it => it.Type == type);*/

            if (transactions.Count == 0)
            {
                Transaction transaction = new Transaction(request.Initials,
                                                          request.Type,
                                                          request.Price,
                                                          request.Quantity);

                transactions.Add(transaction);
            }
            else
            {

                Transaction transaction = transactions.First();

                Transaction deal = new Transaction(request.Initials,
                                                   request.Type,
                                                   request.Price,
                                                   request.Quantity);

                if (transaction != null)
                {
                    Transaction remaining = transaction.SetDeal(deal);

                    if (remaining != null)
                        transactions.Add(remaining);
                }
                else
                    transactions.Add(deal);
            }

            transactions.ToList()
                        .ForEach(it =>
                        {
                            if (it.TransactionId == default)
                                TransactionRepository.Insert(it);
                            else
                                TransactionRepository.Update(it);
                        });

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
