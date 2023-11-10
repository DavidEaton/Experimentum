using Experimentum.Api.Data;
using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons;
using Microsoft.EntityFrameworkCore;

namespace Experimentum.Api.Features.Persons
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext context;

        public PersonRepository(
            ApplicationDbContext context)
        {
            this.context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public void Add(Person person)
        {
            if (person is not null)
                context.Attach(person);
        }

        public void Delete(Person person)
        {
            context.Remove(person);
        }

        public void DeleteEmail(Email email)
        {
            context.Entry(email).State = EntityState.Deleted;
        }

        public async Task<IReadOnlyList<PersonRequest>> GetAllAsync()
        {
            var personsFromContext = await context.Persons
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            return personsFromContext
                .Select(person =>
                        person.ToRequest())
                .ToList();
        }

        public async Task<Person?> GetEntityAsync(long id)
        {
            return await context.Persons
                .AsSplitQuery()
                .FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task<PersonRequest?> GetAsync(long id)
        {
            var personFromContext = await GetEntityAsync(id);

            return personFromContext is not null
                ? personFromContext.ToRequest()
                : null;
        }
    }
}
