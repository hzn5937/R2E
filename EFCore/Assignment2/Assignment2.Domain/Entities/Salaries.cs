namespace Assignment2.Domain.Entities
{
    public class Salaries
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }
                
        public Employees Employee { get; set; }
    }
}
