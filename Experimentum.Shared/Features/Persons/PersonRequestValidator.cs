using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons.PersonNames;
using FluentValidation;

namespace Experimentum.Shared.Features.Persons
{
    public class PersonRequestValidator : AbstractValidator<PersonRequest>
    {
        public PersonRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(person => person.Name)
                .Cascade(CascadeMode.Continue)
                .SetValidator(new PersonNameRequestValidator());

            RuleFor(person => person.Email)
                .Cascade(CascadeMode.Continue)
                .SetValidator(new EmailRequestValidator());

            RuleFor(person => person)
                .MustBeEntity((person) =>
                {
                    var nameResult = PersonName.Create(person.Name.LastName, person.Name.FirstName, person.Name.MiddleName);
                    if (nameResult.IsFailure)
                    {
                        return Result.Failure<Person>(nameResult.Error);
                    }

                    var emailResult = Email.Create(person.Email.Address);
                    if (emailResult.IsFailure)
                    {
                        return Result.Failure<Person>(emailResult.Error);
                    }

                    return Person.Create(
                        nameResult.Value,
                        person.Gender,
                        person.Birthday,
                        person.FavoriteColor,
                        emailResult.Value);
                });
        }
    }
}
