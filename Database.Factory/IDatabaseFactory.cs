using Database.Interface;

namespace Database.Factory
{
    public interface IDatabaseFactory
    {
        IPlayerDatabase PlayerDatabase { get; }
        ITransactionDatabase TransactionDatabase { get; }
    }
}
