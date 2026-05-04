namespace BankManagement.Entities
{
    public class Branch
    {
        public string BranchCode { get; set; } 
        public string BranchName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public Manager Managers { get; set; } 
        public List <Account> Accounts { get; set; } 
    }
}
