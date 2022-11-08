using Database.Interface;
using TransactionEngine.Model;

namespace Database.Implementation
{
    public class TransactionDatabase : ITransactionDatabase
    {
        private object _lock = new object();
        private HashSet<(Player, Transaction)> _transactions = new HashSet<(Player, Transaction)>();

        public Transaction GetTransaction(Guid id)
        {
            lock (_lock)
            {
                return _transactions.SingleOrDefault(x => x.Item2.Id == id).Item2;
            }
        }

        public IEnumerable<Transaction> GetPlayerTransactions(Guid playerId)
        {
            lock (_lock)
            {
                return _transactions.Where(x => x.Item1.Id == playerId).Select(x => x.Item2).ToList();
            }
        }

        public void AddTransaction(Player player, Transaction transaction)
        {
            lock (_lock)
            {
                _transactions.Add((player, transaction));
            }
        }
    }
}