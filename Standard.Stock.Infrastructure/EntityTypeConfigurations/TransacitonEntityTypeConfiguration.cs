using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;

namespace Standard.Stock.Infrastructure.EntityTypeConfigurations
{
    public class TransacitonEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.Property(p => p.TransactionId).HasColumnName("TransactionId");
            builder.Property(p => p.MainTransaction).HasColumnName("MainTransaction");
            builder.Property(p => p.Initials).HasColumnName("Initials");
            builder.Property(p => p.Type).HasColumnName("Type");
            builder.Property(p => p.Price).HasColumnName("Price");
            builder.Property(p => p.Quantity).HasColumnName("Quantity");
            builder.Property(p => p.IsComplete).HasColumnName("IsComplete");
            builder.Property(p => p.Create).HasColumnName("Create");

            builder.HasMany(p => p.Deals).WithOne(p => p.MainTransaction);
        }
    }
}
