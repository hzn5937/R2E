using Assignment2.Data.Models;
using Assignment2.Model.RookieDto;

namespace Assignment2.Data.Interfaces
{
    public interface IRookiesRepository
    {
        List<Person> GetMales();
        List<Person> GetAllRookies();
        List<Person> GetRookiesBornIn(int year);
        List<Person> GetRookiesBornAfter(int year);
        List<Person> GetRookiesBornBefore(int year);
        void AddRookie(RookieInputDto rookie);
        RookieOutputDto GetRookieById(int id);
        void UpdateRookie(RookieInputDto rookie);
        void DeleteRookie(int id);
    }
}
