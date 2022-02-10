using Newtonsoft.Json;
using System.Net;

namespace Banking_SimpleCoreApi
{
    public class AmazonBankingRepository : IAmazonBankingRepository
    {
        private readonly List<TransactionController> transactionControllers = new List<TransactionController>();
        public Boolean Initialised() { return transactionControllers.Any(); }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public record  RawTransaction(int TransactionId ,string TransactionDate , string AccountName ,int AccountId ,double Amount ,string Description );
        
        public void PopulateAccountInformation(DateOnly fromDate, DateOnly toDate)
        {
            if (Initialised())
                transactionControllers.Clear();
            
            var client = new WebClient();
            var rawResponse = client.DownloadString($@"https://s3.ap-southeast-2.amazonaws.com/myprosperity.com.au/Test/Transactions.json");
            var rawTransactions = JsonConvert.DeserializeObject<List<RawTransaction>>(rawResponse);

            rawTransactions.ForEach(tx =>
            {
                DateOnly txDate = DateOnly.Parse(tx.TransactionDate);//For speed, assume this is correct...TODO - Fix

                if(fromDate <= txDate && txDate <= toDate)
                {
                    var txController = transactionControllers.FirstOrDefault(x => x.Account.AccountId == tx.AccountId) ?? new TransactionController(new BankAccount(tx.AccountId, tx.AccountName));
                    if(!transactionControllers.Contains(txController))
                        transactionControllers.Add(txController);

                    txController.AddTransasction(new Transaction(tx.TransactionId, tx.AccountId, Convert.ToDecimal(tx.Amount), txDate, tx.Description));

                }
            }
            );
        }
        

        public List<TransactionSummary> GetTransactionSummaries(DateOnly fromDate, DateOnly toDate)
        {
            var unsortedResults = new List<TransactionSummary>();
            if (!Initialised())
                PopulateAccountInformation(fromDate, toDate);

            transactionControllers.ForEach(txc =>
            {
                decimal totalAmount = 0;
                
                txc.Transactions.ForEach(tx =>
                {
                    totalAmount += tx.Amount;
                });
                unsortedResults.Add(new TransactionSummary(txc.Account.AccountName , totalAmount, txc.Transactions.Count));
            });
            return unsortedResults.OrderBy(x => x.AccountName).ToList();
        }

        public void Summarise(DateOnly fromDate, DateOnly toDate)
        {
            throw new NotImplementedException();
        }

         
    }
}
