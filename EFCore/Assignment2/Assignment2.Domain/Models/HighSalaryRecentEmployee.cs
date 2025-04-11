namespace Assignment2.Domain.Models
{
    public class HighSalaryRecentEmployee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public DateOnly JoinedDate { get; set; }
    }
}
