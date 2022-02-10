namespace Banking_SimpleCoreApi
{
    public interface ITransaction
    {
        int AccountId { get; }

        decimal Amount { get; }
        
        double TransactionId { get; }

        string Description { get; }

        DateOnly TransactionDate { get; }

    }
}