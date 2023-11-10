using CSharpFunctionalExtensions;
using Experimentum.Domain.Features;

namespace Experimentum.Api.Data
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Persons.Any())
            {
                return; // DB has been seeded
            }

            var persons = new[]
            {
            CreatePerson("Doe", "John", "Jacob", Gender.Male, new DateTime(1985, 4, 12), "Blue", "john.doe@example.com"),
            CreatePerson("Johnson", "Sarah", "K", Gender.Female, new DateTime(1990, 8, 5), "Green", "sarah.j@example.com"),
            CreatePerson("Smith", "Alex", null, Gender.Other, new DateTime(1995, 11, 22), "Red", "alex.smith@example.com"),
            CreatePerson("White", "Emily", null, Gender.Female, new DateTime(1988, 3, 16), "Yellow", "emily.white@example.com"),
            CreatePerson("Brown", "Michael", null, Gender.Male, new DateTime(1975, 1, 30), "Purple", "michael.b@example.com")
        };

            foreach (var person in persons.Where(p => p.IsSuccess))
            {
                context.Persons.Add(person.Value);
            }
            context.SaveChanges();
        }

        private static Result<Person> CreatePerson(string lastName, string firstName, string middleName, Gender gender, DateTime birthday, string favoriteColor, string email)
        {
            var nameResult = PersonName.Create(lastName, firstName, middleName);
            var emailResult = Email.Create(email);

            if (nameResult.IsFailure || emailResult.IsFailure)
            {
                return Result.Failure<Person>("Invalid person name or email");
            }

            return Person.Create(nameResult.Value, gender, birthday, favoriteColor, emailResult.Value);
        }
    }
}
