namespace Banking_SimpleCoreApi
{
    public interface IBankingRepository
    {
        Boolean Initialised();
        string GetMyAccountDetails();
        void AlterBalance(int accountId, decimal amount);
        void CreateAccount(int accountId, string accountName);
        decimal GetBalance(int accountId);
            

    }
}