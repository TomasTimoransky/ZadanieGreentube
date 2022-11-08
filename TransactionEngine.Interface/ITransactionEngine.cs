using TransactionEngine.Model;

namespace TransactionEngine.Interface
{
    public interface ITransactionEngine
    {
        void RegisterPlayerWallet(Guid id);
        bool ProcessTransaction(Guid playerId, Guid transactionId, TransactionType type, double amount);
        IEnumerable<Transaction> GetPlayerTransactions(Guid id);
        double GetPlayerBalance(Guid id);
    }
}
