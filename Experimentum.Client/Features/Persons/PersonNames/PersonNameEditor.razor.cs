using Experimentum.Shared.Features.Persons.PersonNames;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons.PersonNames
{
    public partial class PersonNameEditor : ComponentBase
    {
        [Parameter] public PersonNameRequest Name { get; set; }
    }
}
