using Autofac;
using Ideas.Api.Extensions;
using Ideas.Mailing;

namespace Ideas.Api.IoC
{
    public class MailingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MailingClient>()
                .As<IMailingClient>()
                .InstancePerAspNetCoreRequest();
        }
    }
}
