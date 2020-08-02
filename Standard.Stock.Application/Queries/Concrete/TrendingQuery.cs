using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Standard.Framework.Result.Abstraction;
using Standard.Framework.Result.Concrete;
using Standard.Stock.Application.Queries.Abstraction;
using Standard.Stock.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Standard.Stock.Application.Queries.Concrete
{
    public class TrendingQuery : ITrendingQuery
    {
        private IConfiguration Configuration { get; }

        public TrendingQuery(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IApplicationResult<TrendingResponseViewModel[]> Get(TrendingRequestViewModel request)
        {
            IApplicationResult<TrendingResponseViewModel[]> result = new ApplicationResult<TrendingResponseViewModel[]>();
            IDictionary<string, object> paramters = new Dictionary<string, object>() 
            {
                { "@Initials", request.Initials },
                { "@Create", request.Create ?? DateTime.Now }
            };

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"))) 
            {
                result.Result = connection.Query<TrendingResponseViewModel>(RawSqls.Tradings(), paramters).ToArray();
            }

            return result;
        }
    }
}
