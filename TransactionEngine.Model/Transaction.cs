namespace TransactionEngine.Model
{
    public class Transaction
    {
        public Guid Id { get; }
        public TransactionType TransactionType { get; }
        public double Amount { get; }
        public bool Result { get; }

        public Transaction(Guid id, TransactionType transactionType, double amount, bool result)
        {
            Id = id;
            TransactionType = transactionType;
            Amount = amount;
            Result = result;
        }
    }
}
