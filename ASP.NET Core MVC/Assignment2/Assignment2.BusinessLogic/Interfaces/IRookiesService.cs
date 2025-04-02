using Assignment2.Data.Models;
using Assignment2.Model.RookieDto;

namespace Assignment2.BusinessLogic.Interfaces
{
    public interface IRookiesService
    {
        List<RookieOutputDto> GetMales();
        RookieOutputDto GetOldestRookie();
        List<RookieOutputDto> GetAllRookies();
        List<RookieOutputDto> GetRookiesBornIn(int year);
        List<RookieOutputDto> GetRookiesBornAfter(int year);
        List<RookieOutputDto> GetRookiesBornBefore(int year);
        void AddRookie(RookieInputDto rookie);
        void UpdateRookie(RookieInputDto rookie);
        void DeleteRookie(int id);
        RookieOutputDto GetRookieById(int id);
        PaginatedRookieOutputDto GetPaginatedRookies(int pageNum);
        Stream GetExcel();
    }
}
