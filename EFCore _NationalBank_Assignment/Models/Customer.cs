namespace BankManagement.Entities
{
    public abstract class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public CustomerType CustomerType { get; set; }

        public List <CustomerAccount> customerAccounts { get; set; } 
    }
}
