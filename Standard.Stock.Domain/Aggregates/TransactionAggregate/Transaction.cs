using Standard.Framework.Seedworks.Domains.Abstraction;
using Standard.Stock.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Standard.Stock.Domain.Aggregates.TransactionAggregate
{
    public class Transaction : Entity, IAggregateRoot
    {
        public Guid TransactionId { get; private set; }
        public Guid? MainTransactionId { get; private set; }
        public string Initials { get; private set; }
        public TransactionType Type { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public bool IsComplete => CheckCompletion();
        public DateTime Create { get; private set; }

        public List<Transaction> Deals { get; private set; } = new List<Transaction>();
        public Transaction MainTransaction { get; private set; }

        public Transaction(Guid mainTransactionId,
                           string initials,
                           TransactionType type,
                           double price,
                           int quantity)
        {
            TransactionId = Guid.NewGuid();
            MainTransactionId = mainTransactionId;
            Initials = initials;
            Type = type;
            Price = price;
            Quantity = quantity;
        }

        public Transaction(string initials,
                           TransactionType type,
                           double price,
                           int quantity)
        {
            TransactionId = Guid.NewGuid();
            Initials = initials;
            Type = type;
            Price = price;
            Quantity = quantity;
        }

        public Transaction SetDeal(Transaction deal)
        {
            Transaction result = null;

            double totalPrice = Quantity * Price;
            double done = Deals.Select(it => it.Quantity * it.Price).Sum();
            double left = totalPrice - done;
            double required = deal.Quantity * deal.Price;

            if (left >= required)
            {
                deal.MainTransactionId = TransactionId;
                Deals.Add(deal);
            }
            else
            {
                int leftQuantity = Deals.Sum(it => it.Quantity) - deal.Quantity;

                Transaction part = new Transaction(TransactionId,
                                                   deal.Initials,
                                                   deal.Type,
                                                   deal.Price,
                                                   leftQuantity);
                Deals.Add(part);

                result = new Transaction(deal.Initials,
                                         deal.Type,
                                         deal.Price,
                                         deal.Quantity = leftQuantity);
            }

            return result;
        }

        private bool CheckCompletion() => Deals.Select(it => it.Quantity * it.Price).Sum() == (Quantity * Price);
    }
}
