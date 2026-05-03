namespace BankManagement.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!; 
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }

      
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
