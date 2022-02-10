using Banking_SimpleCoreApi.Setup;
using Banking_SimpleCoreApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Banking_SimpleCoreApi.Common.EndpointDefinitions
{
    public class AmazonTransactionEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("BankAccountsSummaryDebug", GetBankAccountSummaryDebug);
            app.MapPost("BankAccountsSummary", GetBankAccountSummary);
        } 
        internal IResult GetBankAccountSummary(IAmazonBankingRepository repo, DateOnly fromDate, DateOnly toDate)
        {
            var accountSummaries =  repo.GetTransactionSummaries(fromDate, toDate);
            return Results.Ok(accountSummaries);
        }
        internal IResult GetBankAccountSummaryDebug(IAmazonBankingRepository repo)
        {
            var accountSummaries = repo.GetTransactionSummaries(DateOnly.MinValue, DateOnly.MaxValue);
            return Results.Ok(accountSummaries);
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<IAmazonBankingRepository, AmazonBankingRepository>();
        }

    }
}
