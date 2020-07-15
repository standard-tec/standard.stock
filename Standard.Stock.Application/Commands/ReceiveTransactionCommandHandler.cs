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
    public class ReceiveTransactionCommandHandler : IRequestHandler<ReceiveTransactionCommand, IApplicationResult<string>>
    {
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
            List<Transaction> transactions = TransactionRepository.Get(null, true);

            if (transactions.Count == 0)
            {
                Transaction transaction = new Transaction(request.Initials,
                                                          (TransactionType)request.Type,
                                                          request.Price,
                                                          request.Quantity);

                transactions.Add(transaction);
            }
            else
            {
                TransactionType type = (TransactionType)request.Type == TransactionType.Buy ? TransactionType.Sell : TransactionType.Buy;
                Transaction transaction = transactions.FirstOrDefault(it => it.Initials == request.Initials && 
                                                                            it.Type == type && 
                                                                            !it.IsComplete);

                Transaction deal = new Transaction(request.Initials,
                                                   (TransactionType)request.Type,
                                                   request.Price,
                                                   request.Quantity);

                if (transaction != null)
                {
                    Transaction remaining = transaction.SetDeal(deal);

                    if (remaining != null)
                        transactions.Add(deal);
                }
                else
                    transactions.Add(deal);
            }

            transactions.Where(it => it.TransactionId == Guid.Empty)
                        .ToList()
                        .ForEach(it => TransactionRepository.Insert(it));
            
            transactions.Where(it => it.TransactionId != Guid.Empty)
                        .ToList()
                        .ForEach(it => TransactionRepository.Update(it));

            await Context.SaveChangesAsync();

            result.Result = "Transaction received";
            return result;
        }
    }
}
