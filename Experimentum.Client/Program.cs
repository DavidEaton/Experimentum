using Experimentum.Client;
using Experimentum.Client.Features.Persons;
using Experimentum.Shared.Features.Persons;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

const string apiUrl = "https://localhost:7216/api/";
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IPersonDataService, PersonDataService>(
    client => client.BaseAddress = new Uri(apiUrl));

builder.Services.AddTransient<IValidator<PersonRequest>, PersonRequestValidator>();

await builder.Build().RunAsync();
