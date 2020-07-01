using System;
using System.Collections.Generic;

namespace Standard.Stock.Domain.Aggregates.TransactionAggregate
{
    public interface ITransactionRepository
    {
        void Insert(Transaction model);
        List<Transaction> Get(DateTime create);
    }
}
