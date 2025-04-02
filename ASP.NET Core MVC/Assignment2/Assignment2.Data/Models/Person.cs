using Assignment2.Model;    

namespace Assignment2.Data.Models
{
    public class Person
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ToString()
        {
            return $"Id: {Id}, Name: {FirstName} {LastName}, Gender: {Gender}, Date of Birth: {DateOfBirth}, Phone Number: {PhoneNumber}, Birth Place: {BirthPlace}, Is Graduated: {IsGraduated}";
        }
        public string FullName()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
