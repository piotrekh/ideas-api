using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Ideas.Models;
using Ideas.Domain.Ideas.Queries;

namespace Ideas.Domain.Ideas.Validators
{
    public class GetIdeaDetailsValidatorPipeline : ValidationBehavior<GetIdeaDetails, IdeaDetails>
    {
        public GetIdeaDetailsValidatorPipeline() : base(new GetIdeaDetailsValidator()) { }
    }

    public class GetIdeaDetailsValidator : AbstractValidator<GetIdeaDetails>
    {
        public GetIdeaDetailsValidator()
        {
            RuleFor(x => x.IdeaId).IsValidIntId().WithErrorCode(Error.InvalidIdeaId.ToString());
        }
    }
}
