using Autofac.Builder;

namespace Ideas.Api.Extensions
{
    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InstancePerAspNetCoreRequest<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder)
        {
            // when using Autofac in Asp.Net Core we need to use
            // InstancePerLifetimeScope instead of InstancePerRequest
            // to configure dependencies lifetime as per request
            // http://docs.autofac.org/en/latest/integration/aspnetcore.html#id4
            return registrationBuilder.InstancePerLifetimeScope();
        }
    }
}
