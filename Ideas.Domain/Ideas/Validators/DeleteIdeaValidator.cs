using FluentValidation;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Common.Validation;
using Ideas.Domain.Ideas.Commands;

namespace Ideas.Domain.Ideas.Validators
{
    public class DeleteIdeaValidatorPipeline : ValidationBehavior<DeleteIdea>
    {
        public DeleteIdeaValidatorPipeline() : base(new DeleteIdeaValidator()) { }
    }

    public class DeleteIdeaValidator : AbstractValidator<DeleteIdea>
    {
        public DeleteIdeaValidator()
        {
            RuleFor(x => x.IdeaId).IsValidIntId().WithErrorCode(Error.InvalidIdeaId.ToString());
        }
    }
}
