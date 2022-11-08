using TransactionEngine.Model;

namespace Database.Interface
{
    public interface IPlayerDatabase
    {
        bool AddPlayer(Guid id);

        Player GetPlayer(Guid id);
    }
}