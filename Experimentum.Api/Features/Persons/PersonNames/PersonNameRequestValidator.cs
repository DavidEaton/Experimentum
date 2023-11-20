using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons.PersonNames;
using FluentValidation;

namespace Experimentum.Api.Features.Persons.PersonNames
{
    public class PersonNameRequestValidator : AbstractValidator<PersonNameRequest>
    {
        public PersonNameRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(personName => personName)
            .MustBeValueObject(
                name => PersonName.Create(
                    name.LastName,
                    name.FirstName,
                    name.MiddleName));
        }
    }
}
