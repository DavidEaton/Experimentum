using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons.PersonNames;
using FluentValidation;

namespace Experimentum.Client.Features.Persons.PersonNames
{
    public class PersonNameRequestValidator : AbstractValidator<PersonNameRequest>
    {
        public PersonNameRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(name => name.FirstName)
                .NotEmpty()
                .WithMessage(PersonName.RequiredMessage)
                .Length(PersonName.MinimumLength, PersonName.MaximumLength)
                .WithMessage(PersonName.InvalidLengthMessage);

            RuleFor(name => name.LastName)
                .NotEmpty()
                .WithMessage(PersonName.RequiredMessage)
                .Length(PersonName.MinimumLength, PersonName.MaximumLength)
                .WithMessage(PersonName.InvalidLengthMessage);

            RuleFor(name => name.MiddleName)
                .Length(PersonName.MinimumLength, PersonName.MaximumLength)
                .WithMessage(PersonName.InvalidLengthMessage)
                .When(name => !string.IsNullOrWhiteSpace(name.MiddleName));
        }
    }
}
