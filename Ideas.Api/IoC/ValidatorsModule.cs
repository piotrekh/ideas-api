using Autofac;
using FluentValidation;
using Ideas.Domain.Categories.Validators;
using Ideas.Domain.Common.Extensions;
using Ideas.Domain.Common.Validation;
using System.Linq;
using System.Reflection;

namespace Ideas.Api.IoC
{
    public class ValidatorsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesAssembly = typeof(CreateCategoryValidator).GetTypeInfo().Assembly;

            //register validators
            //builder.RegisterAssemblyTypes(servicesAssembly)
            //    .Where(x => x.IsSubclassOfRawGeneric(typeof(AbstractValidator<>)))
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //register validators pipelines
            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.IsSubclassOfRawGeneric(typeof(ValidationBehavior<,>)))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
