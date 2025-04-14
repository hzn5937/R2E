using Assignment2.Data.Models;
using Assignment2.Model;

namespace Assignment2.Data.DataGenerator
{
    public static class DummyDataGenerator
    {
        public static List<Person> GetRookies()
        {
            List<Person> people = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FirstName = "Dang",
                    LastName = "Bach",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(1998,9,8),
                    PhoneNumber = "123456789",
                    BirthPlace = "Vietnam",
                    IsGraduated = true,
                    CreatedAt = DateTime.Now,
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Ta",
                    LastName = "Tung",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(2004,9,21),
                    PhoneNumber = "234567891",
                    BirthPlace = "Vietnam",
                    IsGraduated = false,
                    CreatedAt = DateTime.Now,
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Nguyen",
                    LastName = "Huy",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(2004,2,1),
                    PhoneNumber = "345678912",
                    BirthPlace = "Vietnam",
                    IsGraduated = false,
                    CreatedAt = DateTime.Now,
                },
                new Person
                {
                    Id = 4,
                    FirstName = "Dang",
                    LastName = "Linh",
                    Gender = Gender.Female,
                    DateOfBirth = new DateOnly(1998,5,1),
                    PhoneNumber = "456789123",
                    BirthPlace = "Vietnam",
                    IsGraduated = true,
                    CreatedAt = DateTime.Now,
                },
                new Person
                {
                    Id = 5,
                    FirstName = "Nguyen",
                    LastName = "Anh",
                    Gender = Gender.Female,
                    DateOfBirth = new DateOnly(2000,3,1),
                    PhoneNumber = "567891234",
                    BirthPlace = "Vietnam",
                    IsGraduated = false,
                    CreatedAt = DateTime.Now,
                },
                new Person
                {
                    Id = 6,
                    FirstName = "Dang",
                    LastName = "Anh",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(2000,3,1),
                    PhoneNumber = "567891234",
                    BirthPlace = "Vietnam",
                    IsGraduated = true,
                    CreatedAt = DateTime.Now,
                }
            };

            return people;
        }
    }
}
