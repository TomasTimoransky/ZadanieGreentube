using Database.Interface;
using TransactionEngine.Model;

namespace Database.Implementation
{
    public class PlayerDatabase : IPlayerDatabase
    {
        private object _lock = new object();
        private HashSet<Player> _players = new HashSet<Player>();

        public bool AddPlayer(Guid id)
        {
            lock (_lock)
            {
                if(!_players.Any(x => x.Id == id))
                {
                    _players.Add(new Player(id));
                    return true;
                }
                return false;
            }      
        }

        public Player GetPlayer(Guid id)
        {
            lock (_lock)
            {
                return _players.SingleOrDefault(x => x.Id == id);
            }
        }
    }
}