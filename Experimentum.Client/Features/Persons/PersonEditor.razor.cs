using Experimentum.Shared.Features.Persons;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        private readonly PersonRequest person = new();
        private EditContext? _editContext;
        [Inject] private IValidator<PersonRequest> _personValidator { get; set; }

        private string emailValidationMessage = string.Empty;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(person);
        }
        private void SubmitForm()
        {
            ValidateForm();
        }

        private void PartialValidate()
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            var validationResult = _personValidator.Validate(person);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!string.IsNullOrWhiteSpace(error.ErrorMessage) && !string.IsNullOrWhiteSpace(error.PropertyName))
                    {
                        if (error.PropertyName == nameof(person.Email))
                        {
                            emailValidationMessage = error.ErrorMessage;
                        }
                    }
                }
            }
            else
            {
                emailValidationMessage = string.Empty;
            }
        }

        private string EmailInputCssClass =>
            !string.IsNullOrWhiteSpace(emailValidationMessage)
            ? "form-control is-invalid"
            : "form-control";

    }
}
