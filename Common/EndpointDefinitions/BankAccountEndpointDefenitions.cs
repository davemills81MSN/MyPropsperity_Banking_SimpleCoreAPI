using Banking_SimpleCoreApi.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Banking_SimpleCoreApi;

public class BankAccountEndpointDefenition: IEndpointDefinition
{ 
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/initialise",Initialise);
        app.MapPost("/MyBankAccounts", GetMyBankAccounts);
        app.MapPost("/createaccount/{id}/{name}", CreateAccount);
        app.MapPost("/getaccountbalance/{id}",GetAccountBalance);

    }

    //internal IResult ConvertCurrency()

    internal IResult GetMyBankAccounts(IBankingRepository repo)
    {
        if(!repo.Initialised())
            Initialise(repo);
        return Results.Ok(repo.GetMyAccountDetails());
    }

    internal IResult Initialise(IBankingRepository repo)
    {
        if (!repo.Initialised())
        { 
            repo.CreateAccount(1, "Account 1");
            repo.AlterBalance(1, 100);
            repo.CreateAccount(2, "Account 2");
            repo.AlterBalance(2, 200);
        }

        return Results.Ok("Initialised 2 bank accounts");
    }

    internal IResult CreateAccount(IBankingRepository repo, int accountId, string accountName)
    {
        repo.CreateAccount(accountId, accountName);
        return Results.CreatedAtRoute($@"/createaccount/{accountId}/{accountName}");
    }

    internal IResult GetAccountBalance(IBankingRepository repo, int accountId)
    {
        try { 
            var accountBalance = repo.GetBalance(accountId);
            return Results.Ok(accountBalance);
        }
        catch{
            return Results.NotFound();
        }
            
    }
    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IBankingRepository,BankingRepository>() ;
    }
}

