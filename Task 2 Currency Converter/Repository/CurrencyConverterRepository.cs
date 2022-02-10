namespace Banking_SimpleCoreApi;

using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class CurrencyConverterRepository : ICurrencyConverterRepository
{
    
    public string GetConversionRate(string sourceCurrency)
    {
        var client = new WebClient();
        var response = client.DownloadString($@"http://apilayer.net/api/live?access_key=69b0dbad7d9244c001a337943c32dc8d&currencies={sourceCurrency},GBP,CAD,AUD&source=USD&format=1");
        
        //NOTE :- The response from the api was access restricted as I owned the free account for this demo
        // var response = client.DownloadString($@"http://api.currencylayer.com/convert?access_key=69b0dbad7d9244c001a337943c32dc8d&from={sourceCurrency}&to=AUD&amount=1");
        //{"success":false,"error":{"code":105,"info":"Access Restricted - Your current Subscription Plan does not support this API Function."}}
        // 
        //Because the 
        
        //First tried JSONSoft but need to define the class structure properly 
        //"{\n  \"success\":true,\n  \"terms\":\"https:\\/\\/currencylayer.com\\/terms\",\n  \"privacy\":\"https:\\/\\/currencylayer.com\\/privacy\",\n  \"timestamp\":1644394262,\n  \"source\":\"USD\",\n  \"quotes\":{\n    \"USDGBP\":0.738025,\n    \"USDCAD\":1.270925,\n    \"USDAUD\":1.397888\n  }\n}"
        //Method divide the USDsourceCurrency by the USDAUD
        ;

        return response.ToString();
    }
}
