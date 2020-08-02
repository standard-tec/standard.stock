using System;
using System.Runtime.Serialization;

namespace Standard.Stock.Application.ViewModels
{
    [DataContract(Name = "tranding")]
    public class TrendingResponseViewModel
    {
        [DataMember(Name = "initials")]
        public string Initials { get; set; }

        [DataMember(Name = "totalOfBuys")]
        public int TotalOfBuys { get; set; }

        [DataMember(Name = "totalOfSells")]
        public int TotalOfSells { get; set; }

        [DataMember(Name = "totalOfTransactions")]
        public int TotalOfTransactions { get { return TotalOfBuys + TotalOfSells; } }

        [DataMember(Name = "average")]
        public decimal Average { get; set; }

        [DataMember(Name = "trending")]
        public decimal Trending { get { return CalculateTrend(); } }

        private decimal CalculateTrend() 
        {
            decimal buy = (decimal) TotalOfBuys / TotalOfTransactions;
            decimal sell = (decimal) TotalOfSells / TotalOfTransactions;

            return Math.Round(buy - sell, 2);
        }
    }
}
