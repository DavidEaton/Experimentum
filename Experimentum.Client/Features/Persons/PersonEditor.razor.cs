using Experimentum.Shared.Features.Persons;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        private readonly PersonRequest person = new();
        [Inject] private IValidator<PersonRequest>? PersonValidator { get; set; }

        public PersonEditor() { }

        private void SubmitValidForm()
        {
            TrimPersonRequest();
            Console.WriteLine("SubmitValidForm() called: Form Submitted Successfully!");
        }
        private void ValidateForm()
        {
            TrimPersonRequest();
            var validationResult = PersonValidator?.Validate(person);
            Console.WriteLine($"ValidateForm() called... validationResult: {validationResult}");
        }

        private void TrimPersonRequest()
        {
            person.Name.FirstName = person.Name.FirstName?.Trim();
            person.Name.LastName = person.Name.LastName?.Trim();
            person.Name.MiddleName = person.Name.MiddleName?.Trim();
            person.FavoriteColor = person.FavoriteColor?.Trim();
            person.Email.Address = person.Email.Address?.Trim();
        }
    }
}
