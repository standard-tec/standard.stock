using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standand.Framework.MessageBroker.Concrete;
using Standand.Framework.MessageBroker.Concrete.Options;
using Standard.Framework.Seedworks.Abstraction.Events;
using Standard.Stock.Application.Commands;
using Standard.Stock.Application.Queries.Abstraction;
using Standard.Stock.Application.Queries.Concrete;
using Standard.Stock.Domain.Aggregates.TransactionAggregate;
using Standard.Stock.Infrastructure.Contexts;
using Standard.Stock.Infrastructure.Repositories;
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
            Assembly assembly = typeof(ReceiveTransactionCommand).GetTypeInfo().Assembly;

            builder.Register(ctx =>
            {
                return Configuration;
            })
            .As<IConfiguration>()
            .SingleInstance();

            builder.Register(ctx =>
            {
                IOptions<BrokerOptions> broker = ctx.Resolve<IOptions<BrokerOptions>>();
                ILifetimeScope scope = ctx.Resolve<ILifetimeScope>();
                
                return new EventBus(scope, broker);
            })
            .As<IEventBus>()
            .SingleInstance();

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<>))
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IIntegrationEventHandler<>))
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IIntegrationEventHandler<,>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<TransactionRepository>()
                   .As<ITransactionRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<TrendingQuery>()
                   .As<ITrendingQuery>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<TransactionQuery>()
                   .As<ITransactionQuery>()
                   .InstancePerLifetimeScope();

            builder.Register(ctx =>
            {
                DbContextOptionsBuilder<StockContext> options = new DbContextOptionsBuilder<StockContext>();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), (provider) => provider.EnableRetryOnFailure());
                options.EnableDetailedErrors(true);

                return new StockContext(options.Options);
            })
            .As<StockContext>()
            .InstancePerLifetimeScope();
        }
    }
}
