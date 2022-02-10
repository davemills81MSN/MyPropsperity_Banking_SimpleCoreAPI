
namespace Banking_SimpleCoreApi
{
    public interface IAmazonBankingRepository
    {
        bool Initialised();
        void PopulateAccountInformation(DateOnly fromDate, DateOnly toDate);
        List<TransactionSummary> GetTransactionSummaries(DateOnly fromDate, DateOnly toDate);
    }
}