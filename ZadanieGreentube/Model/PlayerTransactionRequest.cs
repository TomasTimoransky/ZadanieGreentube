using TransactionEngine.Model;

namespace ZadanieGreentube.Model
{
    public class TransactionRequest
    {
        public Guid PlayerId { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
    }
}
