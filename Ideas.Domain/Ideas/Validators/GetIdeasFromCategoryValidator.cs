using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;

namespace Ideas.Domain.Ideas.Validators
{
    public class GetIdeasFromCategoryValidatorPipeline : ValidationBehavior<GetIdeasFromCategory, ItemsResult<Idea>>
    {
        public GetIdeasFromCategoryValidatorPipeline() : base(new GetIdeasFromCategoryValidator()) { }
    }

    public class GetIdeasFromCategoryValidator : AbstractValidator<GetIdeasFromCategory>
    {
        public GetIdeasFromCategoryValidator()
        {
            RuleFor(x => x.CategoryId).IsValidIntId().WithErrorCode(Error.InvalidCategoryId.ToString());
        }
    }
}
