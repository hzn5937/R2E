using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Data.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }

        public string ToString()
        {
            return $"Name: {FirstName} {LastName}, Gender: {Gender}, Date of Birth: {DateOfBirth}, Phone Number: {PhoneNumber}, Birth Place: {BirthPlace}, Is Graduated: {IsGraduated}";
        }
        public string FullName()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
