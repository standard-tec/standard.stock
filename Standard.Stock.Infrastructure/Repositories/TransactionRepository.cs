using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Standard.Stock.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private StockContext Context { get; }

        public TransactionRepository(StockContext context) => Context = context;

        public List<Transaction> Get(DateTime? create, bool? isIncomplete)
        {
            return Context.Transactions
                          .Where(it => it.Create == create || it.IsComplete == isIncomplete)
                          .ToList();
        }

        public void Insert(Transaction model)
        {
            Context.Transactions.Add(model);
        }

        public void Update(Transaction model)
        {
            Context.Transactions.Update(model);
        }
    }
}
