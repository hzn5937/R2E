namespace Assignment2.Application.DTOs.Employees
{
    public class EmployeeOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public DateOnly JoinedDate { get; set; }
    }
}
