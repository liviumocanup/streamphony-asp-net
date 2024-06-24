using FluentValidation;

namespace Streamphony.Infrastructure.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, DateOnly> DateNotInFuture<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder.Must(date => date <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("{PropertyName} cannot be in the future");
    }

    public static IRuleBuilderOptions<T, string?> ValidUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("{PropertyName} is not a valid URL");
    }
}
