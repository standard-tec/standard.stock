using Standard.Framework.Seedworks.Concrete.Events;
using System.Runtime.Serialization;

namespace Standard.Stock.Event
{
    [DataContract(Name = "receive-transaction")]
    public class ReceiveTransactionEvent : IntegrationEvent
    {
        [DataMember(Name = "initials")]
        public string Initials { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
    }
}
