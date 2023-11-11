using Experimentum.Domain.Features;
using FluentValidation;

namespace Experimentum.Shared.Features.Persons.PersonNames
{
    public class PersonNameRequestValidator : AbstractValidator<PersonNameRequest>
    {
        public PersonNameRequestValidator()
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
