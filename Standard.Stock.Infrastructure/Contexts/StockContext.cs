using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Standard.Stock.Infrastructure.Contexts
{
    public class StockContext : DbContext
    {
        DbSet<Transaction> Transactions { get; set; }

    }
}
