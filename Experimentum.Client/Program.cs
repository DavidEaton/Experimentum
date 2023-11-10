using Experimentum.Client;
using Experimentum.Client.Features.Persons;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

const string apiUrl = "https://localhost:7216/api/";
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IPersonDataService, PersonDataService>(
    client => client.BaseAddress = new Uri(apiUrl));

await builder.Build().RunAsync();
