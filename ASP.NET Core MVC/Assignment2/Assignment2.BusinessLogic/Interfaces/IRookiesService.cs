using Assignment2.Data.Models;
using Assignment2.Model.RookieDto;

namespace Assignment2.BusinessLogic.Interfaces
{
    public interface IRookiesService
    {
        List<Person> GetMales();
        Person GetOldestRookie();
        List<Person> GetAllRookies();
        List<Person> GetRookiesBornIn(int year);
        List<Person> GetRookiesBornAfter(int year);
        List<Person> GetRookiesBornBefore(int year);
        void AddRookie(RookieInputDto rookie);
        void UpdateRookie(RookieInputDto rookie);
        void DeleteRookie(int id);
        RookieOutputDto GetRookieById(int id);
        Stream GetExcel();
    }
}
