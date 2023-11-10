using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons.PersonNames;
using FluentValidation;

namespace Experimentum.Shared.Features.Persons
{
    public class PersonValidator : AbstractValidator<PersonRequest>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Name)
                .SetValidator(new PersonNameValidator());

            RuleFor(person => person.Email)
                .SetValidator(new EmailValidator());

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
