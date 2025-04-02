using Assignment2.Data.DataGenerator;
using Assignment2.Data.Interfaces;
using Assignment2.Data.Models;
using Assignment2.Model.RookieDto;
using Assignment2.Model;

namespace Assignment2.Data.Repositories
{
    public class RookiesRepository : IRookiesRepository
    {
        private List<Person> _rookies;
        private int _newId = 0;
        public RookiesRepository()
        {
            _rookies = DummyDataGenerator.GetRookies();
            _newId = _rookies.Max(p => p.Id) + 1;
        }

        public void DeleteRookie(int id)
        {
            Person person = _rookies.Where(p => p.Id == id).First();
            _rookies.Remove(person);
        }

        public void UpdateRookie(RookieInputDto rookie)
        {
            Person person = _rookies.Where(p => p.Id == rookie.Id).First();

            person.FirstName = rookie.FirstName;
            person.LastName = rookie.LastName;
            person.Gender = rookie.Gender;
            person.DateOfBirth = rookie.DateOfBirth;
            person.PhoneNumber = rookie.PhoneNumber;
            person.BirthPlace = rookie.BirthPlace;
            person.IsGraduated = rookie.IsGraduated;
        }

        public RookieOutputDto GetRookieById(int id)
        {
            // throw exception if null;
            Person? person = _rookies.Where(p => p.Id == id).FirstOrDefault() ?? throw new ArgumentException("Rookie not found");

            RookieOutputDto output = new RookieOutputDto()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                BirthPlace = person.BirthPlace,
                IsGraduated = person.IsGraduated
            };

            return output;
        }

        public void AddRookie(RookieInputDto rookie)
        {
            int id = _rookies.Max(p => p.Id) + 1;
            Person person = new Person()
            {
                Id = _newId++,
                FirstName = rookie.FirstName,
                LastName = rookie.LastName,
                Gender = rookie.Gender,
                DateOfBirth = rookie.DateOfBirth,
                PhoneNumber = rookie.PhoneNumber,
                BirthPlace = rookie.BirthPlace,
                IsGraduated = rookie.IsGraduated,
                CreatedAt = DateTime.Now
            };
            _rookies.Add(person);
        }

        public List<RookieOutputDto> GetAllRookies()
        {
            List<RookieOutputDto> output = _rookies.Select(p => new RookieOutputDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BirthPlace = p.BirthPlace,
                    IsGraduated = p.IsGraduated
                })
                .ToList();

            return output;
        }

        public List<RookieOutputDto> GetMales()
        {
            List<RookieOutputDto> maleRookies = _rookies.Where(p => p.Gender.Equals(Gender.Male))
                .Select(p => new RookieOutputDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BirthPlace = p.BirthPlace,
                    IsGraduated = p.IsGraduated
                })
                .ToList();

            return maleRookies;
        }

        public List<RookieOutputDto> GetRookiesBornAfter(int year)
        {
            List<RookieOutputDto> rookiesBornAfter = _rookies.Where(p => p.DateOfBirth.Year > year)
                .Select(p => new RookieOutputDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BirthPlace = p.BirthPlace,
                    IsGraduated = p.IsGraduated
                }).ToList();

            return rookiesBornAfter;
        }

        public List<RookieOutputDto> GetRookiesBornBefore(int year)
        {
            List<RookieOutputDto> rookiesBornBefore = _rookies.Where(p => p.DateOfBirth.Year < year)
                .Select(p => new RookieOutputDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BirthPlace = p.BirthPlace,
                    IsGraduated = p.IsGraduated
                }).ToList();

            return rookiesBornBefore;
        }

        public List<RookieOutputDto> GetRookiesBornIn(int year)
        {
            List<RookieOutputDto> rookiesBornIn = _rookies.Where(p => p.DateOfBirth.Year == year)
                .Select(p => new RookieOutputDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BirthPlace = p.BirthPlace,
                    IsGraduated = p.IsGraduated
                }).ToList();

            return rookiesBornIn;
        }
    }
}
