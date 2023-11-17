using Blazored.FluentValidation;
using Experimentum.Shared.Features.Persons;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        //[Parameter] public PersonRequest? Person { get; set; }
        private readonly PersonRequest? Person = new();
        [Parameter] public EventCallback Close { get; set; }
        [Parameter] public EventCallback Save { get; set; }
        private FluentValidationValidator? _fluentValidationValidator;
        private async Task SubmitFormAsync()
        {
            var validationResult = await _fluentValidationValidator!.ValidateAsync();

            if (validationResult)
            {
                Console.WriteLine("Form Submitted Successfully!");
            }
        }

        private void PartialValidate()
        {
            var validationResult = _fluentValidationValidator!.Validate(options => options.IncludeAllRuleSets());
            Console.WriteLine($"Partial validation result : {validationResult}");
        }
    }
}
