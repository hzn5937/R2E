using Assignment2.Domain.Enums;

namespace Assignment2.Application.DTOs.Person
{
    public class PersonUpdateDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string BirthPlace { get; set; } = null!;
    }
}
