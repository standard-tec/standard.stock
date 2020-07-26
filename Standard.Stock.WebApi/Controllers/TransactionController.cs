using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.Framework.Result.Abstraction;
using Standard.Stock.Application.Commands;
using Standard.Stock.Application.Queries.Abstraction;
using Standard.Stock.Application.ViewModels;
using System.Threading.Tasks;

namespace Standard.Stock.WebApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : Controller
    {
        private IMediator Mediator { get; }
        private ITrandingQuery TrandingQuery { get; }

        public TransactionController(IMediator mediator, ITrandingQuery trandingQuery) 
        {
            Mediator = mediator;
            TrandingQuery = trandingQuery;
        }

        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<string>))]
        public async Task<IActionResult> Post([FromBody]ReceiveTransactionCommand command) => await Mediator.Send(command);

        [HttpGet("trandings"), Produces("application/json", Type = typeof(IApplicationResult<TrandingResponseViewModel[]>))]
        public async Task<IActionResult> Get([FromQuery]TrandingRequestViewModel request) => TrandingQuery.Get(request);
    }
}
