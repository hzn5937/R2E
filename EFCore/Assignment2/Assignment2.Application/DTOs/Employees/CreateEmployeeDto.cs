namespace Assignment2.Application.DTOs.Employees
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }
    }
}
