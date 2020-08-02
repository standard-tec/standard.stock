using Standard.Framework.Result.Abstraction;
using Standard.Stock.Application.ViewModels;

namespace Standard.Stock.Application.Queries.Abstraction
{
    public interface ITrendingQuery
    {
        IApplicationResult<TrendingResponseViewModel[]> Get(TrendingRequestViewModel request);
    }
}
