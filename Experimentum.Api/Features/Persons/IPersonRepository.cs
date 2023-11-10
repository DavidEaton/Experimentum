using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Persons;

namespace Experimentum.Api.Features.Persons
{
    public interface IPersonRepository
    {
        void Add(Person entity);
        void Delete(Person entity);
        Task<IReadOnlyList<PersonRequest>> GetAllAsync();
        Task<PersonRequest?> GetAsync(long id);
        Task<Person?> GetEntityAsync(long id);
        Task SaveChangesAsync();
        void DeleteEmail(Email entity);
    }
}
