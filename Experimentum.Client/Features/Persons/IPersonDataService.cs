using CSharpFunctionalExtensions;
using Experimentum.Shared.Features.Persons;

namespace Experimentum.Client.Features.Persons
{
    public interface IPersonDataService
    {
        Task<Result<IReadOnlyList<PersonResponse>>> GetAllAsync();
        Task<Result<PersonResponse>> GetAsync(long id);
        Task<Result> AddAsync(PersonRequest person);
        Task<Result> UpdateAsync(PersonRequest person);
    }
}
