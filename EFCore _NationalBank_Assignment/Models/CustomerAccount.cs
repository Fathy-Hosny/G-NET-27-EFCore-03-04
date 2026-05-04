namespace BankManagement.Entities
{
    public class CustomerAccount
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public OwnershipRole OwnershipRole { get; set; }
        public DateTime LinkedDate { get; set; } = DateTime.Now;
    }
}
