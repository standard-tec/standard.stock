using Dapper;
using Microsoft.Extensions.Configuration;
using Standard.Framework.Result.Abstraction;
using Standard.Framework.Result.Concrete;
using Standard.Stock.Application.Queries.Abstraction;
using Standard.Stock.Application.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Standard.Stock.Application.Queries.Concrete
{
    public class TransactionQuery : ITransactionQuery
    {
        private IConfiguration Configuration { get; }

        public TransactionQuery(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public async Task<IApplicationResult<TransactionResponseViewModel[]>> GetAsync()
        {
            IApplicationResult<TransactionResponseViewModel[]> result = new ApplicationResult<TransactionResponseViewModel[]>();
            IDictionary<string, object> paramters = new Dictionary<string, object>();

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                result.Result = connection.Query<TransactionResponseViewModel>(RawSqls.Transactions, paramters).ToArray();
            }

            return result;
        }
    }
}
