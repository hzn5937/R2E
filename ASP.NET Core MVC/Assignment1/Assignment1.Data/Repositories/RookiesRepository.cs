using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.Data.DataGenerator;
using Assignment1.Data.Interfaces;
using Assignment1.Data.Models;
using Microsoft.Extensions.Logging;

namespace Assignment1.Data.Repositories
{
    public class RookiesRepository : IRookiesRepository
    {
        private readonly ILogger<RookiesRepository> _logger;

        public RookiesRepository(ILogger<RookiesRepository> logger)
        {
            _logger = logger;
        }

        public List<Person> GetAllRookies()
        {
            try
            {
                List<Person> rookies = DummyDataGenerator.GetRookies();

                return rookies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public List<Person> GetMales()
        {
            try
            {
                List<Person> rookies = GetAllRookies();

                List<Person> maleRookies = rookies.Where(p => p.Gender.Equals(Gender.Male)).ToList();

                return maleRookies;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public List<Person> GetRookiesBornAfter(int year)
        {
            try
            {
                List<Person> rookies = GetAllRookies();

                List<Person> rookiesBornAfter = rookies.Where(p => p.DateOfBirth.Year > year).ToList();

                return rookiesBornAfter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public List<Person> GetRookiesBornBefore(int year)
        {
            try
            {
                List<Person> rookies = GetAllRookies();

                List<Person> rookiesBornBefore = rookies.Where(p => p.DateOfBirth.Year < year).ToList();

                return rookiesBornBefore;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public List<Person> GetRookiesBornIn(int year)
        {
            try
            {
                List<Person> rookies = GetAllRookies();

                List<Person> rookiesBornIn = rookies.Where(p => p.DateOfBirth.Year == year).ToList();

                return rookiesBornIn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }
    }
}
