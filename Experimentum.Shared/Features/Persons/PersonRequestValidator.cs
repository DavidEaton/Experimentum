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

            IRuleBuilderOptions<PersonRequest, PersonNameRequest> personNameResult = RuleFor(person => person.Name)
                .Cascade(CascadeMode.Continue)
                .SetValidator(new PersonNameRequestValidator());

            IRuleBuilderOptions<PersonRequest, EmailRequest> personEmailResult = RuleFor(person => person.Email)
                .Cascade(CascadeMode.Continue)
                .SetValidator(new EmailRequestValidator());

            IRuleBuilderOptions<PersonRequest, PersonRequest> personResult = RuleFor(person => person)
                .MustBeEntity((person) =>
                {
                    // If either Name or Email value objects are null, Person.Create
                    // will never run, and remaining validations will not be performed.
                    // Instead, create fake valid values for Name and Email to ensure that
                    // Person.Create will run and all validations will be performed.
                    return Person.Create(
                        PersonName.Create("lastName", "firstName").Value,
                        person.Gender,
                        person.Birthday,
                        person.FavoriteColor,
                        Email.Create("e@mail.com")
                    .Value);
                });

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
