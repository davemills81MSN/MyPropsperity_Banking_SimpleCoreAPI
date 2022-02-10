
namespace Banking_SimpleCoreApi
{
    public interface ICurrencyConverterRepository
    {
        public string GetConversionRate(string sourceCurrency);
    }
}