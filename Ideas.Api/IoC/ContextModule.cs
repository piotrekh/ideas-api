using Autofac;
using Ideas.Api.Context;
using Ideas.Api.Extensions;
using System.Reflection;

namespace Ideas.Api.IoC
{
    public class ContextModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesAssembly = typeof(UserContext).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.Name.EndsWith("Context"))
                .AsImplementedInterfaces()
                .InstancePerAspNetCoreRequest();
        }
    }
}
