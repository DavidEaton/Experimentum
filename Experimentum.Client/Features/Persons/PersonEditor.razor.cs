using Experimentum.Shared.Features.Persons;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonEditor : ComponentBase
    {
        [Parameter] public PersonRequest? Person { get; set; }
        [Parameter] public EventCallback Close { get; set; }
        [Parameter] public EventCallback Save { get; set; }
        private async Task HandleValidSubmit()
        {
            throw new NotImplementedException();
        }

        private void ValidateField()
        {
            Console.WriteLine("ValidateField() called");
            //ParentEditContext.NotifyFieldChanged(new FieldIdentifier(Person, nameof(Person)));
        }
    }
}
