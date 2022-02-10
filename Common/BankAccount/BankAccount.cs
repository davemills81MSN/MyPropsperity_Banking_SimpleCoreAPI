namespace Banking_SimpleCoreApi
{

    public record BankAccount(int AccountId, string AccountName) : IBankAccount;

}
