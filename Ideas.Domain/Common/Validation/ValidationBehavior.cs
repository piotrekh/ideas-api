using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Domain.Common.Validation
{  
    public class ValidationBehavior<TRequest, TResponse> : ValidationBehaviorBase<TRequest, TResponse>
    {
        public ValidationBehavior(IValidator<TRequest> validator) : base(validator) { }
    }

    public class ValidationBehavior<TRequest> : ValidationBehaviorBase<TRequest, Unit>
    {
        public ValidationBehavior(IValidator<TRequest> validator) : base(validator) { }
    }

    public abstract class ValidationBehaviorBase<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviorBase(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            if (_validator == null)
                return next();

            var context = new ValidationContext(request);
            var failures = _validator.Validate(context).Errors;

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}
