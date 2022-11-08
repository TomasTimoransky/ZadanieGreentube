using Database.Implementation;
using Database.Interface;

namespace Database.Factory
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private PlayerDatabase _playerDatabase = new PlayerDatabase();
        public IPlayerDatabase PlayerDatabase => _playerDatabase;

        private TransactionDatabase _transactionDatabase = new TransactionDatabase();
        public ITransactionDatabase TransactionDatabase => _transactionDatabase;
    }
}