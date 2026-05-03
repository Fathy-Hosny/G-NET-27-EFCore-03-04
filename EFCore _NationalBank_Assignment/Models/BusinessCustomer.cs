namespace BankManagement.Entities
{
    public class BusinessCustomer : Customer
    {
        public string CompanyName { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string Industry { get; set; } = null!;
    }
}
