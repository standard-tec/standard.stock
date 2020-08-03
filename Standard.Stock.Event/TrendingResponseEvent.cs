using Standard.Framework.Seedworks.Concrete.Events;
using System.Runtime.Serialization;

namespace Standard.Stock.Event
{
    [DataContract(Name = "tranding")]
    public class TrendingResponseEvent : IntegrationEvent
    {
        public TrendingResponseEvent() { }

        [DataMember(Name = "trending-items")]
        public TrendingItemEvent[] Trendings { get; set; }
    }
}
