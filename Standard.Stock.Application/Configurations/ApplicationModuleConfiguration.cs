using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Standard.Stock.Application.Configurations
{
    public class ApplicationModuleConfiguration : Autofac.Module
    {
        public IConfiguration Configuration { get; }

        public ApplicationModuleConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            ConfigureInstancePerLifetimeScope(builder, Configuration);
        }

        public void ConfigureInstancePerLifetimeScope(ContainerBuilder builder, IConfiguration configuration) 
        {
        
        }
    }
}
