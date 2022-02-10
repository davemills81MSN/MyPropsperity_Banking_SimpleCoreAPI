namespace Banking_SimpleCoreApi
{
    public class AmazonTransactionController
    {
        public BankAccount Account { get; }
        public List<Transaction> Transactions { get;  }

        public AmazonTransactionController(BankAccount account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            Transactions = new List<Transaction>();
            Account = account;
        }
        public void AdjustBalance(decimal amount)
        {
            Transactions.Add( new Transaction(Account.AccountId, amount));
        }

    }
}
