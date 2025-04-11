namespace Assignment2.Application.DTOs.ProjectEmployees
{
    public class ProjectEmployeeOutputDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool Enable { get; set; }
    }
}
