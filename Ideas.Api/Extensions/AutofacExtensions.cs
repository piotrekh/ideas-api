using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Ideas.Api.Extensions
{
    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<TLimit, ScanningActivatorData, DynamicRegistrationStyle> InstancePerAspNetCoreRequest<TLimit>(this IRegistrationBuilder<TLimit, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder)
        {
            // when using Autofac in Asp.Net Core we need to use
            // InstancePerLifetimeScope instead of InstancePerRequest
            // to configure dependencies lifetime as per request
            // http://docs.autofac.org/en/latest/integration/aspnetcore.html#id4
            return registrationBuilder.InstancePerLifetimeScope();
        }
    }
}
