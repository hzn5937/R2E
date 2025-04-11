namespace Assignment2.Domain.Entities
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProjectEmployees> ProjectEmployees { get; set; } = new List<ProjectEmployees>();
    }
}
