using Microsoft.AspNetCore.Builder;
using Banking_SimpleCoreApi;
using System.Reflection;
using Banking_SimpleCoreApi.Setup;

var builder = WebApplication.CreateBuilder(args);

var endpointAssembliesToInclude = new Assembly[] { typeof(BankingRepository).Assembly };
builder.Services.AddEndpointDefinitionsFromAssemblies(endpointAssembliesToInclude);

var app = builder.Build();  

app.UseEndpointDefinitions();
    
app.Run();


