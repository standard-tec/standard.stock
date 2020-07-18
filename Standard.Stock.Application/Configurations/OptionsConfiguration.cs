using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Standand.Framework.MessageBroker.Concrete.Options;

namespace Standard.Stock.Application.Configurations
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<BrokerOptions>(options => configuration.GetSection("brokerOptions").Bind(options));
        }
    }
}
