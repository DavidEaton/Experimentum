using CSharpFunctionalExtensions;
using FluentValidation;
using Entity = Experimentum.Domain.Abstractions.Entity;

namespace Experimentum.Shared.Features
{
    public static class ApplicationValidators
    {
        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TValueObject, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {

                Result<TValueObject> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error);
                }
            });
        }

        public static IRuleBuilderOptions<T, TElement> MustBeEntity<T, TElement, TEntity>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TEntity>> factoryMethod)
            where TEntity : Entity
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TEntity> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error);
                }
            });
        }
    }
}
