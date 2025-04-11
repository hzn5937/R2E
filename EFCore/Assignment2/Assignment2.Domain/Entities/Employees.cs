namespace Assignment2.Domain.Entities
{
    public class Employees
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }

        public Departments Department { get; set; }
        public Salaries? Salary { get; set; }
        public ICollection<ProjectEmployees> ProjectEmployees { get; set; } = new List<ProjectEmployees>();
    }
}
