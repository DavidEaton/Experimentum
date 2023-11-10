using Experimentum.Domain.Features;
using FluentValidation;

namespace Experimentum.Shared.Features.Persons.PersonNames
{
    public class PersonNameValidator : AbstractValidator<PersonNameRequest>
    {
        public PersonNameValidator()
        {
            RuleFor(personName => personName)
            .MustBeValueObject(
                name => PersonName.Create(
                    name.LastName,
                    name.FirstName,
                    name.MiddleName));
        }
    }
}
