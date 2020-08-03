using Standard.Stock.Application.ViewModels;
using Standard.Stock.Event;
using System.Linq;

namespace Standard.Stock.Application.Mappers
{
    public static class TrendingMapper
    {
        public static TrendingRequestViewModel MapTo(this TrendingRequestEvent @event) 
        {
            return new TrendingRequestViewModel()
            {
                Create = @event.Create,
                Initials = @event.Initials
            };
        }

        public static TrendingResponseEvent MapTo(this TrendingResponseViewModel[] viewModels) 
        {
            return new TrendingResponseEvent() { Trendings = viewModels.Select(MapTo).ToArray() };
        }

        private static TrendingItemEvent MapTo(this TrendingResponseViewModel viewModel) 
        {
            return new TrendingItemEvent() 
            {
                Average = viewModel.Average,
                Initials = viewModel.Initials,
                TotalOfBuys = viewModel.TotalOfBuys,
                TotalOfSells = viewModel.TotalOfSells,
                TotalOfTransactions = viewModel.TotalOfTransactions,
                Trending = viewModel.Trending
            };
        }
    }
}
