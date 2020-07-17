using Autofac;
using MediatR;
using System.Reflection;

namespace Standard.Stock.Application.Configurations
{
    public class MediatorModuleConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext componentContext = ctx.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
