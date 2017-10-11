using FluentValidation;

namespace Ideas.Domain.Common.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidIntId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x =>
            {
                int id = -1;
                bool success = int.TryParse(x, out id);

                return success && id > 0;
            });
        }
    }
}
