using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Ideas.Commands;
using Ideas.Domain.Ideas.Models;

namespace Ideas.Domain.Ideas.Validators
{
    public class CreateIdeaValidatorPipeline : ValidationBehavior<CreateIdea, IdeaDetails>
    {
        public CreateIdeaValidatorPipeline() : base(new CreateIdeaValidator()) { }
    }

    public class CreateIdeaValidator : AbstractValidator<CreateIdea>
    {
        public CreateIdeaValidator()
        {
            RuleFor(x => x.CategoryId).IsValidIntId().WithErrorCode(Error.InvalidCategoryId.ToString());
            RuleFor(x => x.Title).NotEmpty().WithErrorCode(Error.InvalidIdeaTitle.ToString());
            RuleFor(x => x.Subcategories).Must(x => x == null || x.Count <= 5).WithErrorCode(Error.TooManySubcategories.ToString());
        }
    }
}
