using Experimentum.Api.Data;
using Experimentum.Api.Features.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.TryAddScoped<IPersonRepository, PersonRepository>();

services.AddHealthChecks();
services.AddCors(o => o.AddPolicy("AllowAll", policyBuilder =>
{
    policyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"DatabaseSettings:MigrationsConnection"])
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information));

var app = builder.Build();

app.UseHttpsRedirection();
// Call UseCors before UseRouting and UseEndpoints.
app.UseCors("AllowAll");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/healthcheck");
});

app.Run();