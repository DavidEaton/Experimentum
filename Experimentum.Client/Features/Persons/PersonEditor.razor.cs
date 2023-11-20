using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        private readonly PersonRequest person = new();
        private EditContext? editContext;
        [Inject] private IValidator<PersonRequest>? PersonValidator { get; set; }
        [Inject] private IValidator<EmailRequest>? EmailValidator { get; set; }

        public PersonEditor() { }
        public PersonEditor(IValidator<PersonRequest>? personValidator)
        {
            PersonValidator = personValidator;
        }

        protected override void OnInitialized()
        {
            editContext = new EditContext(person);
        }
        private void SubmitForm()
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            var validationResult = PersonValidator?.Validate(person);

            if (validationResult is null)
            {
                return;
            }


        }
    }
}
