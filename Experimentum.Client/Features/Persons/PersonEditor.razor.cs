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
        public PersonEditor(IValidator<PersonRequest>? personValidator)
        {
            PersonValidator = personValidator;
        }

        private void SubmitValidForm()
        => Console.WriteLine("SubmitValidForm() called: Form Submitted Successfully!");

        private void ValidateForm()
        {
            var validationResult = PersonValidator?.Validate(person);
            Console.WriteLine($"ValidateForm() called... validationResult: {validationResult}");
        }
    }
}
