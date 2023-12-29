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
        public async Task<ActionResult> UpdateAsync(PersonRequest request)
        {
            Person? person = await repository.GetEntityAsync(request.Id);
            if (person is null)
                return NotFound($"Could not find {request.Name.FirstName} {request.Name.LastName} to update");

            UpdateName(request, person);

            person.SetGender(request.Gender);
            person.SetBirthday(request.Birthday);
            person.SetFavoriteColor(request.FavoriteColor);
            person.SetEmail(Email.Create(request.Email.Address).Value);

            UpdatePhones(request, person);

            await repository.SaveChangesAsync();

            return NoContent();
        }

        private void UpdatePhones(PersonRequest request, Person person)
        {
            foreach (var phone in person.Phones)
            {
                var phoneRequest = request.Phones.FirstOrDefault(p => p.Id == phone.Id);

                if (phoneRequest is null)
                {
                    person.RemovePhone(phone);
                }
                else
                {

                    if (phoneRequest.Number != phone.Number)
                    {
                        phone.SetNumber(phoneRequest.Number);
                    }

                    if (phoneRequest.PhoneType != phone.PhoneType)
                    {
                        phone.SetPhoneType(phoneRequest.PhoneType);
                    }
                }
            }

            foreach (var phoneRequest in request.Phones
                .Where(p => p.Id == 0))
            {
                var phone = Phone.Create(phoneRequest.Number, phoneRequest.PhoneType).Value;
                person.AddPhone(phone);
            }
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
