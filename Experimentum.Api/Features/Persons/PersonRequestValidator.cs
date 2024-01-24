using Experimentum.Api.Features.Emails;
using Experimentum.Api.Features.Persons.PersonNames;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons;
using FluentValidation;

namespace Experimentum.Api.Features.Persons
{
    public class PersonRequestValidator : AbstractValidator<PersonRequest>
    {
        public PersonRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;

            RuleFor(person => person.Name)
                .SetValidator(new PersonNameRequestValidator());

            RuleFor(person => person.Email)
                .SetValidator(new EmailRequestValidator());

            // If either Name or Email validations fail, Person.Create
            // will never run, and remaining validations will not be performed.
            // Instead, create fake valid values for Name and Email to ensure that
            // Person.Create will run and all validations will be performed.
            RuleFor(person => person)
                .MustBeEntity(
                    person =>
                    Person.Create(
                        PersonName.Create("lastName", "firstName").Value,
                        person.Gender,
                        person.Birthday,
                        person.FavoriteColor,
                        Email.Create("e@mail.com")
                    .Value));

            // combine the results of the PersonName, Email, and Person validators
            // to ensure that all validations are performed and all error messages
            // are returned
            //RuleFor(person => person)
            //    .Cascade(CascadeMode.Stop)
            //    .Must((person) =>
            //    {
            //        return personNameResult && personEmailResult && personResult;
            //    })
            //    .WithMessage("Please correct the errors below.");
        }
    }
}
