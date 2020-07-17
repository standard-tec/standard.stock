using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Standard.Stock.WebApi.Controllers
{
    public class TransactionController : Controller
    {
        private IMediator Mediator { get; }

        public TransactionController(IMediator mediator) 
        {
            Mediator = mediator;
        }

    }
}
