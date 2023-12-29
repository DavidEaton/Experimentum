using CSharpFunctionalExtensions;
using Experimentum.Client.Shared;
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

        public async Task<Result> AddAsync(PersonRequest person)
        {
            try
            {
                var result = await httpClient.AddAsync(
                    UriSegment,
                    person);

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure($"An unexpected error occurred: {ex.Message}");
            }
        }

        public async Task<Result> UpdateAsync(PersonRequest fromCaller)
        {
            return await httpClient.UpdateAsync(
                UriSegment,
                fromCaller,
                person => $"{fromCaller.Name.FirstName} {fromCaller.Name.LastName}",
                person => person.Id);
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
