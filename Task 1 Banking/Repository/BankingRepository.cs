using Newtonsoft.Json;

namespace Banking_SimpleCoreApi
{
    class BankingRepository : IBankingRepository
    {
        private readonly List<TransactionController> bankAccounts = new List<TransactionController>();
        
        public Boolean Initialised() { return bankAccounts.Any(); }
        
        public string GetMyAccountDetails()
        {
            return JsonConvert.SerializeObject(bankAccounts);
        }

        /// <summary>
        /// Create a new Bank Account
        /// </summary>
        /// <param name="accountId">Must be unique</param>
        /// <param name="accountName"></param>
        public void CreateAccount(int accountId, string accountName)
            {
                var account = new BankAccount(accountId, accountName);
                if (bankAccounts.Exists(x => x.Account.AccountId == accountId))
                    throw new Exception("Cannot create account as account ID already exists");

                bankAccounts.Add(new TransactionController(account));
            }

        /// <summary>
        /// Adjust the balance (+/-) to an existing bank acount.
        /// Note - normally use Debit/Credit etc but this is just a simple app...
        /// </summary>
        /// <param name="accountId">The account must be created and you use the correct account id</param>
        /// <param name="amount">The decimal amount (+/-) to add.</param>
        /// <exception cref="Exception"></exception>
        public void AlterBalance(int accountId, decimal amount)
        {
            var accountController = bankAccounts.FirstOrDefault(x => x.Account.AccountId == accountId);

            if (accountController == null)
                throw new Exception("Account balance cannon't be altered.   Account could not found.");

            accountController.AdjustBalance(amount);
        }

        /// <summary>
        /// The sum of all transactions is the balance
        /// </summary>
        /// <param name="accountId">Account Id must exist</param>
        /// <returns>decimal of the account balance</returns>
        /// <exception cref="Exception"></exception>
        public decimal GetBalance(int accountId)
        {
            var accountController = bankAccounts.FirstOrDefault(x => x.Account.AccountId == accountId);

            if (accountController == null)
                throw new Exception("Account balance cannon't be retrieved. No matching account found");

            return accountController.Transactions.Sum(x => x.Amount);
        }
    }
}