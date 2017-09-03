using Autofac;
using Ideas.Api.Extensions;
using Ideas.Domain.Users.Services;
using System.Linq;
using System.Reflection;

namespace Ideas.Api.IoC
{
    public class DomainServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesAssembly = typeof(UsersService).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerAspNetCoreRequest();
        }
    }
}
