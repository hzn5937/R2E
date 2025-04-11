namespace Assignment2.Domain.Entities
{
    public class ProjectEmployees
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public bool Enable { get; set; }

        public Employees Employee { get; set; }
        public Projects Project { get; set; }
    }
}
