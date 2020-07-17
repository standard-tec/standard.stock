using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standard.Framework.Seedworks.Abstraction.Events;
using Standard.Stock.Application.Options;
using Standard.Stock.Event;

namespace Standard.Stock.Application.Configurations
{
    public static class EventBusConfiguration
    {
        public static void ConfigureEventBus(this IApplicationBuilder app) 
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            IOptions<TransactionOptions> options = app.ApplicationServices.GetRequiredService<IOptions<TransactionOptions>>();
            eventBus.SubscribeAsync<ReceiveTransactionEvent, IIntegrationEventHandler<ReceiveTransactionEvent>>(options.Value);
        }
    }
}
