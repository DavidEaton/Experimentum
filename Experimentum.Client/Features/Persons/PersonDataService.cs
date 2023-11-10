using CSharpFunctionalExtensions;
using Experimentum.Shared.Features.Persons;
using System.Net.Http.Json;

namespace Experimentum.Client.Features.Persons
{
    public class PersonDataService : IPersonDataService
    {
        private readonly HttpClient httpClient;
        private const string UriSegment = "/api/persons";

        public PersonDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Result<PersonResponse>> GetAsync(long id)
        {
            var errorMessage = "Failed to get Person";
            // Ensure httpClient.BaseAddress ends with '/', and UriSegment starts without '/'
            var requestUrl = new Uri(httpClient.BaseAddress, $"{UriSegment}/{id}").ToString();

            try
            {
                var result = await httpClient.GetFromJsonAsync<PersonResponse>(requestUrl);
                return result is not null
                    ? Result.Success(result)
                    : Result.Failure<PersonResponse>(errorMessage);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"{ex.Message} - URL: {requestUrl}");
                return Result.Failure<PersonResponse>(errorMessage);
            }
        }

        public Task<Result> AddAsync(PersonRequest person)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAsync(PersonRequest person)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IReadOnlyList<PersonResponse>>> GetAllAsync()
        {
            var errorMessage = "Failed to get Persons";

            try
            {
                var result = await httpClient.GetFromJsonAsync<IReadOnlyList<PersonResponse>>($"{UriSegment}");
                return result is not null
                    ? Result.Success(result)
                    : Result.Failure<IReadOnlyList<PersonResponse>>(errorMessage);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return Result.Failure<IReadOnlyList<PersonResponse>>(errorMessage);
            }
        }
    }
}
