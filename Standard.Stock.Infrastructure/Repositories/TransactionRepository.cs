using Microsoft.EntityFrameworkCore;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Standard.Stock.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private bool disposed = false;
        private StockContext Context { get; }

        public TransactionRepository(StockContext context) => Context = context;
        
        public async Task<List<Transaction>> Get(params Expression<Func<Transaction, bool>>[] conditions)
        {
            IQueryable<Transaction> transaction = Context.Transactions
                                                         .Include(it => it.Deals)
                                                         .AsQueryable();

            if (conditions?.Length > 0)
                conditions.ToList().ForEach(it => transaction = transaction.Where(it));

            return transaction.ToList();
        }

        public void Insert(Transaction model)
        {
            Context.Transactions.Add(model);
        }

        public void Update(Transaction model)
        {
            Context.Transactions.Update(model);
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
            {
                Context.Dispose();
            }

            disposed = true;
            GC.Collect();
        }
    }
}
