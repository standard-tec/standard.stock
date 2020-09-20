using Microsoft.EntityFrameworkCore;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Infrastructure.EntityTypeConfigurations;
using System;

namespace Standard.Stock.Infrastructure.Contexts
{
    public class StockContext : DbContext, IDisposable
    {
        private bool disposed = false;
        public StockContext(DbContextOptions options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBbuilder) 
        {
            optionsBbuilder.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new TransacitonEntityTypeConfiguration());
        }

        public override void Dispose()
        {
            base.Dispose();
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Transactions = null;
            }

            disposed = true;
            GC.Collect();
        }
    }
}
