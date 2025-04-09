namespace Assignment1.Domain.Entities
{
    public class ProjectEmployee
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public bool Enable { get; set; }

        public Employee Employee { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}
