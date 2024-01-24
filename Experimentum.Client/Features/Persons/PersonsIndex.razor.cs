using CSharpFunctionalExtensions;
using Experimentum.Client.Shared;
using Experimentum.Shared.Features.Persons;
using Microsoft.AspNetCore.Components;

namespace Experimentum.Client.Features.Persons
{
    public partial class PersonsIndex : ComponentBase
    {
        public IReadOnlyList<PersonResponse?> Persons = new List<PersonResponse?>();
        public FormMode PersonFormMode = FormMode.Unknown;
        private PersonRequest? Person { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await PersonDataService.GetAllAsync()
                .Match(
                    success => Persons = success,
                    failure => Console.WriteLine(failure)
            );
        }

        private async Task EditPerson(long id)
        {
            PersonFormMode = FormMode.Edit;
            var result = await PersonDataService.GetAsync(id);
            if (result.IsSuccess)
            {
                Person = result.Value.ResponseToRequest();
            }
        }

        private void AddPerson()
        {
            PersonFormMode = FormMode.Add;
            Person = new PersonRequest();
        }

        private void CancelEdit()
        {
            PersonFormMode = FormMode.View;
            Person = null;
        }

        private async Task SavePerson()
        {
            if (Person is not null)
            {
                var result = Person.Id > 0
                    ? await PersonDataService.UpdateAsync(Person)
                    : await PersonDataService.AddAsync(Person);

                if (result.IsSuccess)
                {
                    PersonFormMode = FormMode.View;
                    Person = null;
                    await OnInitializedAsync();
                }
            }
        }

        private void DeletePerson(long id)
        {
            PersonFormMode = FormMode.View;
            Person = null;
        }
    }
}
