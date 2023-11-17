using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;
using FluentValidation;

namespace Experimentum.Shared.Features.Businesses
{
    public class BusinessRequestValidator : AbstractValidator<BusinessRequest>
    {
        public BusinessRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            //RuleFor(business => business.Name)
            //    .Cascade(CascadeMode.Continue)
            //    .SetValidator(new PersonNameRequestValidator());

            //RuleFor(person => person.Email)
            //    .Cascade(CascadeMode.Continue)
            //    .SetValidator(new EmailRequestValidator());

            RuleFor(business => business)
                .MustBeEntity((business) =>
                {
                    //var nameResult = PersonName.Create(person.Name.LastName, person.Name.FirstName, person.Name.MiddleName);
                    //if (nameResult.IsFailure)
                    //{
                    //    return Result.Failure<Person>(nameResult.Error);
                    //}

                    var emailResult = Email.Create(business.Email);
                    if (emailResult.IsFailure)
                    {
                        return Result.Failure<Business>(emailResult.Error);
                    }

                    //return Person.Create(
                    //    nameResult.Value,
                    //    person.Gender,
                    //    person.Birthday,
                    //    person.FavoriteColor,
                    //    emailResult.Value);
                    return Business.Create(
                        business.Name,
                        emailResult.Value);
                });
        }
    }
}
