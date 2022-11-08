namespace TransactionEngine.Model
{
    public class Player
    {
        public Guid Id { get; }
        public Player(Guid id)
        {
            Id = id;
        }
    }
}
