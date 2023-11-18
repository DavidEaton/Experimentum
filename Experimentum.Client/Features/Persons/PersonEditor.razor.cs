using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons;
using Experimentum.Shared.Features.Persons.PersonNames;
using FluentValidation;
using FluentValidation.Results;
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
        [Inject] private IValidator<PersonNameRequest>? PersonNameValidator { get; set; }

        public PersonEditor() { }
        public PersonEditor(IValidator<PersonRequest>? personValidator)
        {
            PersonValidator = personValidator;
        }

        private string? emailValidationMessage = string.Empty;
        private string? lastNameValidationMessage = string.Empty;
        private string? firstNameValidationMessage = string.Empty;
        private string? middleNameValidationMessage = string.Empty;
        private string? genderValidationMessage = string.Empty;
        private string? birthdayValidationMessage = string.Empty;
        private string? favoriteColorValidationMessage = string.Empty;

        protected override void OnInitialized()
        {
            editContext = new EditContext(person);
        }
        private void SubmitForm()
        {
            ValidateForm();
        }

        private void ValidateField(string propertyName, Action<string> updateValidationMessage)
        {
            var validationResult = PersonValidator?.Validate(person);

            if (validationResult == null || validationResult.IsValid)
            {
                updateValidationMessage(string.Empty);
            }
            else
            {
                string validationMessage = string.Empty;
                foreach (var error in validationResult.Errors)
                {
                    if ((error.PropertyName == propertyName || error.ErrorMessage.Contains(GetSpacedPropertyName(error.PropertyName))) && !string.IsNullOrWhiteSpace(error.ErrorMessage))
                    {
                        validationMessage = error.ErrorMessage;
                        break; // Exit the loop after finding the first relevant error
                    }
                }

                updateValidationMessage(validationMessage);
            }

        }

        private string GetSpacedPropertyName(string propertyName)
        {
            return string.Concat(propertyName.Select((c, i) => i > 0 && char.IsUpper(c) ? $" {c}" : c.ToString()));
        }

        private void ValidateLastName()
        {
            ValidateField(nameof(person.Name.LastName), message => lastNameValidationMessage = message);
        }

        private void ValidateFirstName()
        {
            ValidateField(nameof(person.Name.FirstName), message => firstNameValidationMessage = message);
        }

        private void ValidateMiddleName()
        {
            ValidateField(nameof(person.Name.MiddleName), message => middleNameValidationMessage = message);
        }

        private void ValidateGender()
        {
            ValidateField(nameof(person.Gender), message => genderValidationMessage = message);
        }

        private void ValidateBirthday()
        {
            ValidateField(nameof(person.Birthday), message => birthdayValidationMessage = message);
        }

        private void ValidateFavoriteColor()
        {
            ValidateField(nameof(person.FavoriteColor), message => favoriteColorValidationMessage = message);
        }

        private void ValidateEmail()
        {
            var validationResult = EmailValidator?.Validate(person.Email);

            if (validationResult is null || validationResult.IsValid)
            {
                emailValidationMessage = string.Empty;
            }
            else
            {
                emailValidationMessage = validationResult.Errors
                    .FirstOrDefault(error => error.PropertyName == nameof(person.Email) &&
                                             !string.IsNullOrWhiteSpace(error.ErrorMessage))
                    ?.ErrorMessage ?? string.Empty;
            }
        }

        private void ValidateForm()
        {
            var validationResult = PersonValidator?.Validate(person);

            if (validationResult is null)
            {
                return;
            }

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!string.IsNullOrWhiteSpace(error.ErrorMessage) && !string.IsNullOrWhiteSpace(error.PropertyName))
                    {
                        SetValidationMessages(error);
                    }
                }
            }
            else
            {
                ClearValidationMessages();
            }
        }

        private void SetValidationMessages(ValidationFailure? error)
        {
            if (error.PropertyName == nameof(person.Email))
            {
                emailValidationMessage = error.ErrorMessage;
            }

            if (error.PropertyName == nameof(person.Name))
            {
                lastNameValidationMessage = error.ErrorMessage;
                firstNameValidationMessage = error.ErrorMessage;
                middleNameValidationMessage = error.ErrorMessage;
            }

            if (error.PropertyName == nameof(person.Gender))
            {
                genderValidationMessage = error.ErrorMessage;
            }

            if (error.PropertyName == nameof(person.Birthday))
            {
                birthdayValidationMessage = error.ErrorMessage;
            }

            if (error.PropertyName == nameof(person.FavoriteColor))
            {
                favoriteColorValidationMessage = error.ErrorMessage;
            }
        }

        private void ClearValidationMessages()
        {
            emailValidationMessage = string.Empty;
            lastNameValidationMessage = string.Empty;
            firstNameValidationMessage = string.Empty;
            middleNameValidationMessage = string.Empty;
            genderValidationMessage = string.Empty;
            birthdayValidationMessage = string.Empty;
            favoriteColorValidationMessage = string.Empty;
        }

        private string GenderInputCssClass => GetInputCssClass(genderValidationMessage);
        private string BirthdayInputCssClass => GetInputCssClass(birthdayValidationMessage);
        private string FavoriteColorInputCssClass => GetInputCssClass(favoriteColorValidationMessage);
        private string EmailInputCssClass => GetInputCssClass(emailValidationMessage);
        private string LastNameInputCssClass => GetInputCssClass(lastNameValidationMessage);
        private string FirstNameInputCssClass => GetInputCssClass(firstNameValidationMessage);
        private string MiddleNameInputCssClass => GetInputCssClass(middleNameValidationMessage);

        private string GetInputCssClass(string validationMessage) =>
            !string.IsNullOrWhiteSpace(validationMessage) ? "form-control is-invalid" : "form-control";
    }
}
