using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Standard.Stock.Infrastructure.EntityTypeConfigurations
{
    public class TransacitonEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
        }
    }
}
