using Standard.Framework.Result.Abstraction;
using Standard.Framework.Seedworks.Abstraction.Events;
using Standard.Stock.Application.Mappers;
using Standard.Stock.Application.Queries.Abstraction;
using Standard.Stock.Application.ViewModels;
using Standard.Stock.Event;
using System.Threading.Tasks;

namespace Standard.Stock.Application.IntegrationEvents
{
    public class GetTrendingIntegrationEventHandler : IIntegrationEventHandler<TrendingRequestEvent, TrendingResponseEvent>
    {
        private ITrendingQuery TrandingQuery { get; }

        public GetTrendingIntegrationEventHandler(ITrendingQuery trandingQuery) 
        {
            TrandingQuery = trandingQuery;
        }

        public async Task<TrendingResponseEvent> Handle(TrendingRequestEvent @event)
        {
            TrendingRequestViewModel request = @event.MapTo();
            IApplicationResult<TrendingResponseViewModel[]> result = TrandingQuery.Get(request);

            return result.Result.MapTo();
        }
    }
}
