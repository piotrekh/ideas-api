using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Users.Commands;

namespace Ideas.Domain.Users.Validators
{
    public class ActivateUserValidatorPipeline : ValidationBehavior<ActivateUser>
    {
        public ActivateUserValidatorPipeline() : base(new ActivateUserValidator()) { }
    }

    public class ActivateUserValidator : AbstractValidator<ActivateUser>
    {
        public ActivateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithErrorCode(Error.InvalidEmail.ToString());
            RuleFor(x => x.Token).NotEmpty().WithErrorCode(Error.InvalidToken.ToString());
            RuleFor(x => x.Password).NotEmpty().WithErrorCode(Error.InvalidPassword.ToString());
        }
    }
}
