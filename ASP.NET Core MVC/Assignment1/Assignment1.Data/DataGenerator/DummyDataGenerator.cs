using Assignment1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Data.DataGenerator
{
    internal static class DummyDataGenerator
    {
        public static List<Person> GetRookies()
        {
            List<Person> people = new List<Person>
            {
                new Person
                {
                    FirstName = "Dang",
                    LastName = "Bach",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(1998,9,8),
                    PhoneNumber = "123456789",
                    BirthPlace = "Vietnam",
                    IsGraduated = true,
                },
                new Person
                {
                    FirstName = "Ta",
                    LastName = "Tung",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(2004,9,21),
                    PhoneNumber = "234567891",
                    BirthPlace = "Vietnam",
                    IsGraduated = false,
                },
                new Person
                {
                    FirstName = "Nguyen",
                    LastName = "Huy",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(2004,2,1),
                    PhoneNumber = "345678912",
                    BirthPlace = "Vietnam",
                    IsGraduated = false,
                },
                new Person
                {
                    FirstName = "Dang",
                    LastName = "Linh",
                    Gender = Gender.Female,
                    DateOfBirth = new DateOnly(1998,5,1),
                    PhoneNumber = "456789123",
                    BirthPlace = "Vietnam",
                    IsGraduated = true,
                },
                new Person
                {
                    FirstName = "Nguyen",
                    LastName = "Anh",
                    Gender = Gender.Female,
                    DateOfBirth = new DateOnly(2000,3,1),
                    PhoneNumber = "567891234",
                    BirthPlace = "Vietnam",
                    IsGraduated = false,
                }
            };

            return people;
        }
    }
}
