using Database.Factory;
using Database.Interface;
using TransactionEngine.Model;

namespace TransactionEngine
{
    public class TransactionHelper : ITransactionHelper
    {
        private readonly ITransactionDatabase _transactionDatabase;

        public TransactionHelper(IDatabaseFactory databaseFactory)
        {
            _transactionDatabase = databaseFactory.TransactionDatabase;
        }

        public double GetPlayerBalance(Guid id)
        {
            var transactions = _transactionDatabase.GetPlayerTransactions(id);
            var stake = transactions.Where(x => x.TransactionType == TransactionType.Stake && x.Result == true).Sum(x => x.Amount);
            var depositWin = transactions.Where(x => x.TransactionType == TransactionType.Deposit
            || x.TransactionType == TransactionType.Win && x.Result == true).Sum(x => x.Amount);
            return depositWin - stake;
        }
    }
}
