using Experimentum.Api.Shared;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons;
using Experimentum.Shared.Features.Persons.PersonNames;
using Microsoft.AspNetCore.Mvc;

namespace Experimentum.Api.Features.Persons
{
    public class PersonsController : BaseApplicationController
    {
        private readonly IPersonRepository repository;

        public PersonsController(IPersonRepository repository, ILogger<PersonsController> logger)
        {
            this.repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PersonRequest>>> GetAllAsync()
        {
            var persons = await repository.GetAllAsync();

            return persons is not null
                ? Ok(persons)
                : NotFound();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PersonRequest>> GetAsync(long id)
        {
            var person = await repository.GetAsync(id);

            return person is not null
                ? Ok(person)
                : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PersonRequest>> AddAsync(PersonRequest person)
        {
            var personEntity = person.ToEntity();

            repository.Add(personEntity);

            await repository.SaveChangesAsync();

            return Created(new Uri($"/api/PersonsController/{personEntity.Id}",
                UriKind.Relative),
                new { personEntity.Id });
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            var personFromRepository = await repository.GetEntityAsync(id);

            if (personFromRepository is null)
                return NotFound($"Could not find Person in the database to delete with Id: {id}.");

            repository.Delete(personFromRepository);
            await repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult> UpdateAsync(PersonRequest personFromCaller)
        {
            var personFromRepository = await repository.GetEntityAsync(personFromCaller.Id);
            if (personFromRepository is null)
                return NotFound($"Could not find {personFromCaller.Name.FirstName} {personFromCaller.Name.LastName} to update");

            UpdateName(personFromCaller, personFromRepository);

            personFromRepository.SetGender(personFromCaller.Gender);
            personFromRepository.SetBirthday(personFromCaller.Birthday);
            personFromRepository.SetFavoriteColor(personFromCaller.FavoriteColor);
            personFromRepository.SetEmail(Email.Create(personFromCaller.Email.Address).Value);

            await repository.SaveChangesAsync();

            return NoContent();
        }

        internal static void UpdateName(PersonRequest personFromCaller, Person personFromRepository)
        {
            if (NamesAreNotEqual(personFromRepository.Name, personFromCaller.Name))
            {
                personFromRepository.SetName(PersonName.Create(
                    personFromCaller.Name.LastName,
                    personFromCaller.Name.FirstName,
                    personFromCaller.Name.MiddleName)
                    .Value);
            }
        }

        internal static bool NamesAreNotEqual(PersonName name, PersonNameRequest nameDto) =>
            !string.Equals(name.FirstName, nameDto?.FirstName) ||
            !string.Equals(name.MiddleName, nameDto?.MiddleName) ||
            !string.Equals(name.LastName, nameDto?.LastName);
    }
}
