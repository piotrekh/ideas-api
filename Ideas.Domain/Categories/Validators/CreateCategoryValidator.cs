using FluentValidation;
using Ideas.Domain.Categories.Commands;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Validation;

namespace Ideas.Domain.Categories.Validators
{
    public class CreateCategoryValidatorPipeline : ValidationBehavior<CreateCategory, Category>
    {
        public CreateCategoryValidatorPipeline() : base(new CreateCategoryValidator()) { }
    }

    public class CreateCategoryValidator : AbstractValidator<CreateCategory>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode(Error.InvalidCategoryName.ToString());
        }
    }
}
