using Standard.Framework.Seedworks.Domains.Abstraction;
using System;

namespace Standard.Stock.Domain.StockAggregate
{
    public class Stock : Entity, IAggregateRoot
    {
        public Guid StockId { get; private set; }
        public string Name { get; private set; }
        public string Initials { get; private set; }
        public string Company { get; private set; }
        public DateTime LaunchDate { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public bool Enabled { get; private set; }

        public Stock(string name,
                     string initials,
                     string company,
                     DateTime launchDate,
                     double price,
                     int quantity,
                     bool enabled = true) 
        {
            Name = name;
            Initials = initials;
            Company = company;
            LaunchDate = launchDate;
            Price = price;
            Quantity = quantity;
            Enabled = enabled;
        }
    }
}
