using Experimentum.Client.Shared;
using Experimentum.Shared.Features.Persons;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        [Parameter] public PersonRequest Person { get; set; } = new PersonRequest();
        [Inject] private IValidator<PersonRequest>? PersonValidator { get; set; }
        [Parameter] public FormMode FormMode { get; set; }
        [Parameter] public EventCallback<PersonRequest> OnCancel { get; set; }
        [Parameter] public EventCallback<PersonRequest> OnSubmit { get; set; }
        public PersonEditor() { }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        private void Cancel()
        {
            Person = new PersonRequest();
            FormMode = FormMode.Unknown;
            OnCancel.InvokeAsync(Person);
        }

        private void SubmitValidForm()
        {
            TrimPersonRequest();
            Console.WriteLine("SubmitValidForm() called: Form Submitted Successfully!");
            OnSubmit.InvokeAsync(Person);
        }
        private void ValidateForm()
        {
            TrimPersonRequest();
            var validationResult = PersonValidator?.Validate(Person);
            Console.WriteLine($"ValidateForm() called... validationResult: {validationResult}");
        }

        private void TrimPersonRequest()
        {
            Person.Name.FirstName = Person.Name.FirstName?.Trim();
            Person.Name.LastName = Person.Name.LastName?.Trim();
            Person.Name.MiddleName = Person.Name.MiddleName?.Trim();
            Person.FavoriteColor = Person.FavoriteColor?.Trim();
            Person.Email.Address = Person.Email.Address?.Trim();
        }
    }
}
