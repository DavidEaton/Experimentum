using Experimentum.Client.Shared;
using Experimentum.Shared.Features.Phones;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Phones
{
    public partial class PhoneEditor
    {
        [Parameter]
        public PhoneRequest? Phone { get; set; }

        [Parameter]
        public FormMode FormMode { get; set; } = FormMode.Unknown;

        [Parameter]
        public EventCallback Ok { get; set; }

        [Parameter]
        public EventCallback Cancel { get; set; }

        private void ValidateForm()
        {
            Console.WriteLine("ValidateForm called");
        }
    }
}
