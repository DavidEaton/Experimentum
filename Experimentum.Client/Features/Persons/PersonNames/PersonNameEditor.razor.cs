using Experimentum.Shared.Features.Persons.PersonNames;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons.PersonNames
{
    public partial class PersonNameEditor : ComponentBase
    {
        [Inject] private IValidator<PersonNameRequest>? PersonNameValidator { get; set; }
        [Parameter] public PersonNameRequest personName { get; set; }

        private void Validate()
        {
            var validationResult = PersonNameValidator?.Validate(personName);

            if (validationResult is null)
            {
                return;
            }
        }
    }
}
