using TransactionEngine.Model;

namespace Database.Interface
{
    public interface ITransactionDatabase
    {
        Transaction GetTransaction(Guid id);

        IEnumerable<Transaction> GetPlayerTransactions(Guid playerId);

        void AddTransaction(Player player, Transaction transaction);
    }
}