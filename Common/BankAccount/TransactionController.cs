namespace Banking_SimpleCoreApi
{
    public class TransactionController
    {
        public BankAccount Account { get; }
        public List<Transaction> Transactions { get;  }

        public TransactionController(BankAccount account)
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
        public void AddTransasction(Transaction tx )
        {
            Transactions.Add(tx);
        }

    }
}
