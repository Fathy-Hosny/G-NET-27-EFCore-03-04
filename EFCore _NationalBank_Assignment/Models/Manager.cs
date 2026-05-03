namespace BankManagement.Entities
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime HireDate { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
