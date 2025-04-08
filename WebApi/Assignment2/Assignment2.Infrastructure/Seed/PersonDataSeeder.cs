using Assignment2.Domain.Entities;
using Assignment2.Domain.Enums;

namespace Assignment2.Infrastructure.Seed
{
    public static class PersonDataSeeder
    {
        public static IEnumerable<Person> GetInitialPeople()
        {
            return new List<Person>()
            {
                new Person { Id = 1, FirstName = "Dang", LastName = "Bach", DateOfBirth = new DateOnly(2002, 9, 8), Gender = Gender.Male, BirthPlace = "Ha Noi" },
                new Person {Id = 2, FirstName = "Ta", LastName = "Tung", DateOfBirth = new DateOnly(2004,3,15), Gender = Gender.Male, BirthPlace = "Ha Noi" },
                new Person {Id = 3, FirstName = "Nguyen", LastName = "Huy", DateOfBirth = new DateOnly(2004, 5, 1), Gender = Gender.Male, BirthPlace = "Ha Noi" },
                new Person {Id = 4, FirstName = "Nguyen", LastName = "Khanh", DateOfBirth = new DateOnly(2007, 2, 15), Gender = Gender.Female, BirthPlace = "Bac Ninh" },
                new Person {Id = 5, FirstName = "Nguyen", LastName = "Anh", DateOfBirth = new DateOnly(2000, 5, 17), Gender = Gender.Female, BirthPlace = "Bac Ninh" },
            };
        }
    }
}
