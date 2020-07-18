using Microsoft.EntityFrameworkCore;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Infrastructure.EntityTypeConfigurations;

namespace Standard.Stock.Infrastructure.Contexts
{
    public class StockContext : DbContext
    {

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
    }
}
