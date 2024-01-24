using Experimentum.Client.Shared;
using Experimentum.Shared.Features.Phones;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Phones
{
    public partial class PhonesEditor : ComponentBase
    {
        [Parameter] public List<PhoneRequest>? Phones { get; set; }

        [Parameter] public FormMode FormMode { get; set; }

        public PhoneRequest Phone { get; set; }

        private void Add()
        {
            Phone = new();
            Phones.Add(Phone);
            FormMode = FormMode.Add;
            Console.WriteLine("Add() called");
        }

        private void Edit(PhoneRequest phone)
        {
            if (phone is not null)
            {
                Phone = phone;
                FormMode = FormMode.Edit;
            }
        }

        private void Save()
        {
            if (Phone is not null && FormMode == FormMode.Add)
            {
                Phones?.Add(Phone);
            }

            FormMode = FormMode.View;
        }

        private void Cancel()
        {
            Phone = null;
            FormMode = FormMode.View;
        }

        private void Delete(PhoneRequest phone)
        {
            if (Phone is not null)
            {
                Phones?.Remove(Phone);
                Phone = new();
            }
        }

        private void ValidateForm()
        {
            Console.WriteLine("ValidateForm() called");
        }
    }
}
