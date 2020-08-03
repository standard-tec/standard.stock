using Standard.Framework.Seedworks.Concrete.Events;
using System.Runtime.Serialization;

namespace Standard.Stock.Event
{
    [DataContract(Name = "tranding-item")]
    public class TrendingItemEvent : IntegrationEvent
    {
        public TrendingItemEvent() { }

        [DataMember(Name = "initials")]
        public string Initials { get; set; }

        [DataMember(Name = "totalOfBuys")]
        public int TotalOfBuys { get; set; }

        [DataMember(Name = "totalOfSells")]
        public int TotalOfSells { get; set; }

        [DataMember(Name = "totalOfTransactions")]
        public int TotalOfTransactions { get; set; }

        [DataMember(Name = "average")]
        public decimal Average { get; set; }

        [DataMember(Name = "trending")]
        public decimal Trending { get; set; }
    }
}
