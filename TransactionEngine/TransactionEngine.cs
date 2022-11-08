using Database.Factory;
using Database.Interface;
using TransactionEngine.Interface;
using TransactionEngine.Model;

namespace TransactionEngine
{
    public class TransactionEngine : ITransactionEngine
    {
        private readonly IPlayerDatabase _playerDatabase;
        private readonly ITransactionDatabase _transactionDatabase;
        private readonly ITransactionHelper _transactionHelper;

        public TransactionEngine(IDatabaseFactory databaseFactory, ITransactionHelper transactionHelper)
        {
            _playerDatabase = databaseFactory.PlayerDatabase;
            _transactionDatabase = databaseFactory.TransactionDatabase;
            _transactionHelper = transactionHelper;
        }

        public void RegisterPlayerWallet(Guid id)
        {
            if (!_playerDatabase.AddPlayer(id))
            {
                throw new Exception("Player already exists.");
            }
        }

        public double GetPlayerBalance(Guid id)
        {
            if (_playerDatabase.GetPlayer(id) == null)
            {
                throw new Exception("Player not registered.");
            }

            return _transactionHelper.GetPlayerBalance(id);
        }

        public IEnumerable<Transaction> GetPlayerTransactions(Guid id)
        {
            if (_playerDatabase.GetPlayer(id) == null)
            {
                throw new Exception("Player not registered.");
            }

            return _transactionDatabase.GetPlayerTransactions(id);
        }

        public bool ProcessTransaction(Guid playerId, Guid transactionId, TransactionType type, double amount)
        {
            if(!(type == TransactionType.Stake || type == TransactionType.Deposit || type == TransactionType.Win))
            {
                throw new Exception("Transaction type invalid.");
            }

            if (amount <= 0)
            {
                throw new Exception("Amount invalid.");
            }

            var player = _playerDatabase.GetPlayer(playerId);
            if (player == null)
            {
                throw new Exception("Player not registered.");
            }

            lock (player)
            {
                var transaction = _transactionDatabase.GetTransaction(transactionId);
                if (transaction != null)
                {
                    return transaction.Result;
                }

                var balance = _transactionHelper.GetPlayerBalance(player.Id);
                var newBalance = type == TransactionType.Stake ? balance - amount : balance + amount;
                if (newBalance >= 0)
                {
                    _transactionDatabase.AddTransaction(player, new Transaction(transactionId, type, amount, true));
                    return true;
                }
                else
                {
                    _transactionDatabase.AddTransaction(player, new Transaction(transactionId, type, amount, false));
                    return false;
                }
            }
        }
    }
}
