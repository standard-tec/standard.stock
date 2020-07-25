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
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public bool IsComplete { get; private set; }
        public DateTime Create { get; private set; }

        public List<Transaction> Deals { get; private set; } = new List<Transaction>();
        public Transaction MainTransaction { get; private set; }

        public Transaction() { }

        public Transaction(Guid mainTransactionId,
                           string initials,
                           TransactionType type,
                           decimal price,
                           bool isComplete,
                           int quantity)
        {
            MainTransactionId = mainTransactionId;
            Initials = initials;
            Type = type;
            Price = price;
            Quantity = quantity;
            IsComplete = isComplete;
            Create = DateTime.Now;
        }

        public Transaction(string initials,
                           TransactionType type,
                           decimal price,
                           int quantity)
        {
            Initials = initials;
            Type = type;
            Price = price;
            Quantity = quantity;
            Create = DateTime.Now;
        }

        public Transaction SetDeal(Transaction deal)
        {
            Transaction result = null;

            decimal totalPrice = Quantity * Price;
            decimal done = Deals.Select(it => it.Quantity * it.Price).Sum();
            decimal left = totalPrice - done;
            decimal required = deal.Quantity * deal.Price;

            if (left >= required)
            {
                deal.MainTransactionId = TransactionId;
                deal.SetTransactionComplete();

                Deals.Add(deal);
            }
            else
            {
                int leftQuantity = Quantity - Deals.Sum(it => it.Quantity);

                Transaction part = new Transaction(TransactionId,
                                                   deal.Initials,
                                                   deal.Type,
                                                   deal.Price,
                                                   true,
                                                   leftQuantity);
                Deals.Add(part);

                result = new Transaction(deal.Initials,
                                         deal.Type,
                                         deal.Price,
                                         (deal.Quantity - leftQuantity));
            }

            CheckCompletion();
            return result;
        }

        public void SetTransactionComplete() => IsComplete = true;

        private bool CheckCompletion() 
        {
            bool isComplete = Deals.Select(it => it.Quantity * it.Price).Sum() == (Quantity * Price);
            IsComplete = isComplete;

            return isComplete;
        }
    }
}
