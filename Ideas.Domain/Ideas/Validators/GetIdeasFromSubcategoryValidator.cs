using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;

namespace Ideas.Domain.Ideas.Validators
{
    public class GetIdeasFromSubcategoryValidatorPipeline : ValidationBehavior<GetIdeasFromSubcategory, ItemsResult<Idea>>
    {
        public GetIdeasFromSubcategoryValidatorPipeline() : base(new GetIdeasFromSubcategoryValidator()) { }
    }

    public class GetIdeasFromSubcategoryValidator : AbstractValidator<GetIdeasFromSubcategory>
    {
        public GetIdeasFromSubcategoryValidator()
        {
            RuleFor(x => x.CategoryId).IsValidIntId().WithErrorCode(Error.InvalidCategoryId.ToString());
            RuleFor(x => x.SubcategoryId).IsValidIntId().WithErrorCode(Error.InvalidSubcategoryId.ToString());
        }
    }
}
