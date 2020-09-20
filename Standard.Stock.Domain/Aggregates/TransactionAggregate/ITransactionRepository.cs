using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Standard.Stock.Domain.Aggregates.TransactionAggregate
{
    public interface ITransactionRepository : IDisposable
    {
        void Insert(Transaction model);
        void Update(Transaction model);
        Task<List<Transaction>> Get(params Expression<Func<Transaction, bool>>[] conditions);
    }
}
