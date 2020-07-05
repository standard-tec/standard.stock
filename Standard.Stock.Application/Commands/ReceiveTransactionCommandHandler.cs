using MediatR;
using Standard.Framework.Result.Abstraction;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Stock.Application.Commands
{
    public class ReceiveTransactionCommandHandler : IRequestHandler<ReceiveTransactionCommand, IApplicationResult<string>>
    {
        private ITransactionRepository TransactionRepository { get; }

        public ReceiveTransactionCommandHandler(ITransactionRepository transactionRepository) 
        {
            TransactionRepository = transactionRepository;
        }

        public async Task<IApplicationResult<string>> Handle(ReceiveTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
