using MediatR;
using Standard.Framework.Result.Abstraction;
using Standard.Stock.Domain.Enuns;
using System.Runtime.Serialization;

namespace Standard.Stock.Application.Commands
{
    public class ReceiveTransactionCommand : IRequest<IApplicationResult<string>>
    {
        [DataMember(Name = "initials")]
        public string Initials { get;  set; }

        [DataMember(Name = "type")]
        public TransactionType Type { get; set; }

        [DataMember(Name = "price")]
        public double Price { get; set; }

        [DataMember(Name = "quantity")]

        public int Quantity { get; set; }
    }
}
