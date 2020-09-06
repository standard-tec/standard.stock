using MediatR;
using Standard.Framework.Result.Abstraction;
using Standard.Framework.Result.Concrete;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Domain.Enuns;
using Standard.Stock.Infrastructure.Contexts;
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
            try
            {
                List<Transaction> transactions = TransactionRepository.Get(null, false);

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
                    TransactionType type = request.Type == TransactionType.Buy ? TransactionType.Sell : TransactionType.Buy;
                    Transaction transaction = transactions.FirstOrDefault(it => it.Initials == request.Initials &&
                                                                                it.Type == type &&
                                                                                !it.IsComplete);

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
            }
            catch (System.Exception e)
            {
                throw;
            }

            return result;
        }
    }
}
