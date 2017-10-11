using FluentValidation;
using Ideas.Domain.Categories.Models;
using Ideas.Domain.Categories.Queries;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Models;
using Ideas.Domain.Common.Validation;

namespace Ideas.Domain.Categories.Validators
{
    public class GetSubcategoriesValidatorPipeline : ValidationBehavior<GetSubcategories, ItemsResult<Subcategory>>
    {
        public GetSubcategoriesValidatorPipeline() : base(new GetSubcategoriesValidator()) { }
    }

    public class GetSubcategoriesValidator : AbstractValidator<GetSubcategories>
    {
        public GetSubcategoriesValidator()
        {
            RuleFor(x => x.CategoryId).IsValidIntId().WithErrorCode(Error.InvalidCategoryId.ToString());
        }
    }
}
