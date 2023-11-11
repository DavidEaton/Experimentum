using Blazored.FluentValidation;
using Experimentum.Shared.Features.Persons;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        [Parameter] public PersonRequest? Person { get; set; }
        [Parameter] public EventCallback Close { get; set; }
        [Parameter] public EventCallback Save { get; set; }
        private FluentValidationValidator? _fluentValidationValidator;
        private async Task HandleValidSubmit()
        {
            if (await _fluentValidationValidator!.ValidateAsync())
            {
                Console.WriteLine("Form Submitted Successfully!");
            }
        }

        private void PartialValidate()
                => Console.WriteLine($"Partial validation result : {_fluentValidationValidator?.Validate()}");
    }
}
