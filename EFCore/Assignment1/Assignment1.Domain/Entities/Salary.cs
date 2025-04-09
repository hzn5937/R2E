namespace Assignment1.Domain.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
