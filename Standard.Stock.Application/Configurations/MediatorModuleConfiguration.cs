using Autofac;
using MediatR;
using Standard.Framework.Seedworks.Abstraction.Events;
using Standard.Stock.Application.Commands;
using System.Reflection;

namespace Standard.Stock.Application.Configurations
{
    public class MediatorModuleConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            Assembly assembly = typeof(ReceiveTransactionCommand).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IIntegrationEventHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IIntegrationEventHandler<,>)).InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext componentContext = ctx.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
