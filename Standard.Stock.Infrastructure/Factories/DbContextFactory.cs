using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Standard.Stock.Infrastructure.Contexts;

namespace Standard.Stock.Infrastructure.Factories
{
    public class DbContextFactory : IDesignTimeDbContextFactory<StockContext>
    {
        public StockContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<StockContext> options = new DbContextOptionsBuilder<StockContext>();
            options.UseSqlServer("Data Source=DESKTOP-5MG615U\\SQLEXPRESS;Initial Catalog=Stock;User ID=usrStock;Password=usrStock");

            return new StockContext(options.Options);
        }
    }
}
