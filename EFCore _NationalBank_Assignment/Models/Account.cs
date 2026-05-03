namespace BankManagement.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
        public decimal Balance { get; set; } = 0;
        public DateTime OpenedDate { get; set; } = DateTime.Now;

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

     
        public List <CustomerAccount> CustomerAccounts { get; set; } 
        public List <Transaction> Transactions { get; set; } 
    }
}
