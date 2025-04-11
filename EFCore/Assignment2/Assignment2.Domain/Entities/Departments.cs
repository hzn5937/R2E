namespace Assignment2.Domain.Entities
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employees> Employees { get; set; } = new List<Employees>();
    }
}
