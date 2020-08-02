using Standard.Framework.Seedworks.Concrete.Events;
using System;

namespace Standard.Stock.Event
{
    public class TrendingRequestEvent : IntegrationEvent
    {
        public string Initials { get; set; }
        public DateTime? Create { get; set; }
    }
}
