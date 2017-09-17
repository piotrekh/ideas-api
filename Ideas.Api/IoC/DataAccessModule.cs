using Autofac;
using Ideas.Api.Extensions;
using Ideas.DataAccess;

namespace Ideas.Api.IoC
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerAspNetCoreRequest();
        }
    }
}
