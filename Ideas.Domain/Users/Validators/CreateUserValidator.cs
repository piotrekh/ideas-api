using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Users.Commands;

namespace Ideas.Domain.Users.Validators
{
    public class CreateUserValidatorPipeline : ValidationBehavior<CreateUser>
    {
        public CreateUserValidatorPipeline() : base(new CreateUserValidator()) { }
    }

    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithErrorCode(Error.InvalidEmail.ToString());
            RuleFor(x => x.FirstName).NotEmpty().WithErrorCode(Error.InvalidFirstName.ToString());
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode(Error.InvalidLastName.ToString());
        }
    }
}
