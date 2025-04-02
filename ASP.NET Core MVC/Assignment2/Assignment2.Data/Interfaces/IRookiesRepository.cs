using Assignment2.Data.Models;
using Assignment2.Model.RookieDto;

namespace Assignment2.Data.Interfaces
{
    public interface IRookiesRepository
    {
        List<RookieOutputDto> GetMales();
        List<RookieOutputDto> GetAllRookies();
        List<RookieOutputDto> GetRookiesBornIn(int year);
        List<RookieOutputDto> GetRookiesBornAfter(int year);
        List<RookieOutputDto> GetRookiesBornBefore(int year);
        void AddRookie(RookieInputDto rookie);
        RookieOutputDto GetRookieById(int id);
        void UpdateRookie(RookieInputDto rookie);
        void DeleteRookie(int id);
    }
}
