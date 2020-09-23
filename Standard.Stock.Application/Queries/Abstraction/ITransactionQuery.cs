using Standard.Framework.Result.Abstraction;
using Standard.Stock.Application.ViewModels;
using System.Threading.Tasks;

namespace Standard.Stock.Application.Queries.Abstraction
{
    public interface ITransactionQuery
    {
        Task<IApplicationResult<TransactionResponseViewModel[]>> GetAsync();
    }
}
