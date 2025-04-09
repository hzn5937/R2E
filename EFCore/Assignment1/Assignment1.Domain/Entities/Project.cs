namespace Assignment1.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}
