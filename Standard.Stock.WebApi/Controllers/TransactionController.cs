using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.Framework.Result.Abstraction;
using Standard.Stock.Application.Commands;
using System.Threading.Tasks;

namespace Standard.Stock.WebApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : Controller
    {
        private IMediator Mediator { get; }

        public TransactionController(IMediator mediator) 
        {
            Mediator = mediator;
        }

        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<string>))]
        public async Task<IActionResult> Post([FromBody]ReceiveTransactionCommand command) => await Mediator.Send(command);
    }
}
