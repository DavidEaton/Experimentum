using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features;
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
                    return nameResult.IsFailure
                        ? (Result<Person>)null
                        : Person.Create(
                            nameResult.Value,
                            person.Gender,
                            person.Birthday,
                            person.FavoriteColor,
                            Email.Create(person.Email.Address).Value);
                });
        }
    }
}
