namespace Banking_SimpleCoreApi
{
    public class Transaction : ITransaction
    {
        public Transaction(int accountId, decimal quantity)
        {
            this.AccountId = accountId;
            this.Amount= quantity;
            this.Description = String.Empty;
            this.TransactionDate = DateOnly.MinValue;
            this.TransactionId = -1;
        }
        
        public Transaction(int transactionId, int accountId, decimal amount, DateOnly transactionDate,string description) 
        {
            this.AccountId = accountId;
            this.Amount = amount;
            this.TransactionDate = transactionDate;
            this.Description = description;
            this.TransactionId = transactionId;
        }

        public int AccountId { get; }
        public decimal Amount { get ; }
        public double TransactionId { get ; }
        public string Description { get; }
        public DateOnly TransactionDate { get ; }

    }
}
