using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Emails;
using Experimentum.Shared.Features.Persons.PersonNames;
using Experimentum.Shared.Features.Phones;

namespace Experimentum.Shared.Features.Persons
{
    public static class PersonContractExtensions
    {
        public static Person? ToEntity(this PersonRequest person) =>
            person is null
                ? null
                : Person.Create(
                    PersonName.Create(person.Name.LastName, person.Name.FirstName, person.Name.MiddleName).Value,
                    person.Gender,
                    person.Birthday,
                    person.FavoriteColor,
                    Email.Create(person.Email.Address).Value
                ).Value;

        public static PersonRequest ToRequest(this Person person)
        {
            return person is null
                ? new()
                : new()
                {
                    Id = person.Id,
                    Name = new PersonNameRequest()
                    {
                        FirstName = person.Name.FirstName,
                        LastName = person.Name.LastName,
                        MiddleName = person.Name.MiddleName
                    },
                    Birthday = person.Birthday,
                    Email = new EmailRequest()
                    {
                        Address = person.Email.Address
                    },
                    FavoriteColor = person.FavoriteColor,
                    Gender = person.Gender,
                    Phones = person.Phones
                        .Select(phone => phone.ToRequest())
                        .ToList()
                };
        }

        public static PersonRequest ResponseToRequest(this PersonResponse person)
        {
            return person is null
                ? new()
                : new()
                {
                    Id = person.Id,
                    Name = new PersonNameRequest()
                    {
                        FirstName = person.Name.FirstName,
                        LastName = person.Name.LastName,
                        MiddleName = person.Name.MiddleName
                    },
                    Birthday = person.Birthday,
                    Email = new EmailRequest()
                    {
                        Address = person.Email.Address
                    },
                    FavoriteColor = person.FavoriteColor,
                    Gender = person.Gender,
                    Phones = person.Phones
                        .Select(phone => phone.ToRequest())
                        .ToList()
                };
        }
    }
}
