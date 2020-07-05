using MediatR;
using Standard.Framework.Result.Abstraction;

namespace Standard.Stock.Application.Commands
{
    public class ReceiveTransactionCommand : IRequest<IApplicationResult<string>>
    {
        public string Initials { get;  set; }
        public int Type { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
