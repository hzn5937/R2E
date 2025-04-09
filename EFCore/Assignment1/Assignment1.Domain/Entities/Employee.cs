namespace Assignment1.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }

        public Department Department { get; set; } = null!;
        public Salary? Salary { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }   
}
