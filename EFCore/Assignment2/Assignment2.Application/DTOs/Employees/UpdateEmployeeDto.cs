namespace Assignment2.Application.DTOs.Employees
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }
    }
}
