using MediatR;
using Standard.Framework.Seedworks.Abstraction.Events;
using Standard.Stock.Application.Commands;
using Standard.Stock.Domain.Enuns;
using Standard.Stock.Event;
using System.Threading.Tasks;

namespace Standard.Stock.Application.IntegrationEvents
{
    public class ReceiveTransactionIntegrationEventHandler : IIntegrationEventHandler<ReceiveTransactionEvent>
    {
        private IMediator Mediator { get; }

        public ReceiveTransactionIntegrationEventHandler(IMediator mediator) 
        {
            Mediator = mediator;
        }

        public async Task Handle(ReceiveTransactionEvent @event)
        {
            ReceiveTransactionCommand command = new ReceiveTransactionCommand
            {
                Initials = @event.Initials,
                Price = @event.Price,
                Quantity = @event.Quantity,
                Type = (TransactionType)@event.Type
            };

            await Mediator.Send(command);
        }
    }
}
