using Standard.Framework.Seedworks.Concrete.Events;

namespace Standard.Stock.Event
{
    public class ReceiveTransactionEvent : IntegrationEvent
    {
        public string Initials { get; set; }
        public int Type { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
