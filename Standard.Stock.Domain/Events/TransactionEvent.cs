using Standard.Framework.Seedworks.Concrete.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Standard.Stock.Domain.Events
{
    public class TransactionEvent : IntegrationEvent
    {
        public string Initials { get; private set; }
        public int Type { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
