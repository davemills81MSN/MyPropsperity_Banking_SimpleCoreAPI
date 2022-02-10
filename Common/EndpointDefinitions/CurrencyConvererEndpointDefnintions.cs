using Banking_SimpleCoreApi.Setup;

namespace Banking_SimpleCoreApi;

public class CurrencyConvererEndpointDefnintions: IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/getconversionrate/{sourcecurrency}", GetConversionRate);
        
    }
    
    internal IResult GetConversionRate(ICurrencyConverterRepository repo,string sourceCurrency)
    {
        return Results.Ok(repo.GetConversionRate(sourceCurrency));
    }
    
    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ICurrencyConverterRepository, CurrencyConverterRepository>();
    }
}
