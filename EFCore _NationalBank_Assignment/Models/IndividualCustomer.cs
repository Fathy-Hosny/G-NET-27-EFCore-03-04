namespace BankManagement.Entities
{
    public class IndividualCustomer : Customer
    {
        public string Occupation { get; set; } = null!;
        public string? MaritalStatus { get; set; }
    }
}
