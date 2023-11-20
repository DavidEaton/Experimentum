using Experimentum.Client.Features.Emails;
using Experimentum.Client.Features.Persons.PersonNames;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons;
using FluentValidation;

namespace Experimentum.Client.Features.Persons
{
    public class PersonRequestValidator : AbstractValidator<PersonRequest>
    {
        public PersonRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(person => person.Name)
                .NotEmpty()
                .SetValidator(new PersonNameRequestValidator());

            RuleFor(person => person.Gender)
                .IsInEnum()
                .WithMessage(Person.RequiredMessage);

            RuleFor(person => person.Birthday)
                .Must(birthday => Person.IsValidAgeOn(birthday))
                .WithMessage(Person.InvalidBirthdayMessage)
                .When(person => person.Birthday.HasValue);

            RuleFor(person => person.FavoriteColor)
                .Length(Person.FavoriteColorMinimumLength, Person.FavoriteColorMaximumLength)
                .When(person => !string.IsNullOrWhiteSpace(person.FavoriteColor))
                .WithMessage(Person.FavoriteColorMinimumLengthMessage);

            RuleFor(person => person.Email)
                .NotEmpty()
                .SetValidator(new EmailRequestValidator());
        }
    }
}
