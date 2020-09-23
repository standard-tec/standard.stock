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
        private ITrendingQuery TrandingQuery { get; }
        private ITransactionQuery TransactionQuery { get; }

        public TransactionController(IMediator mediator, 
                                     ITrendingQuery trandingQuery, 
                                     ITransactionQuery transactionQuery) 
        {
            Mediator = mediator;
            TrandingQuery = trandingQuery;
            TransactionQuery = transactionQuery;
        }

        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<string>))]
        public async Task<IActionResult> Post([FromBody]ReceiveTransactionCommand command) => await Mediator.Send(command);

        [HttpGet(), Produces("application/json", Type = typeof(IApplicationResult<TransactionResponseViewModel[]>))]
        public async Task<IActionResult> Get() => await TransactionQuery.GetAsync();

        [HttpGet("trandings"), Produces("application/json", Type = typeof(IApplicationResult<TrendingResponseViewModel[]>))]
        public async Task<IActionResult> Get([FromQuery]TrendingRequestViewModel request) => TrandingQuery.Get(request);
    }
}
