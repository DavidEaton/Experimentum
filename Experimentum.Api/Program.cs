using Experimentum.Api.Data;
using Experimentum.Api.Features.Persons;
using Experimentum.Shared.Features.Persons;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssemblyContaining<PersonRequestValidator>();
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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.EnsureSeedData();
}

app.UseHttpsRedirection();
// Call UseCors BEFORE UseRouting and UseEndpoints.
app.UseCors("AllowAll");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/healthcheck");
});

app.Run();