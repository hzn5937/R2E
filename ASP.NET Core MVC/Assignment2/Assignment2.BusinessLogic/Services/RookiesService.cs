using Assignment2.BusinessLogic.Interfaces;
using Assignment2.Data.Models;
using Assignment2.Data.Interfaces;
using ClosedXML.Excel;
using Assignment2.Model.RookieDto;

namespace Assignment2.BusinessLogic.Services
{
    public class RookiesService : IRookiesService
    {
        private readonly IRookiesRepository _rookiesRepository;

        public RookiesService(IRookiesRepository rookiesRepository)
        {
            _rookiesRepository = rookiesRepository;
        }

        public RookieOutputDto GetRookieById(int id)
        {
            RookieOutputDto rookie = _rookiesRepository.GetRookieById(id);

            return rookie;
        }

        public void DeleteRookie(int id)
        {
            _rookiesRepository.DeleteRookie(id);
        }

        public void UpdateRookie(RookieInputDto rookie)
        {
            _rookiesRepository.UpdateRookie(rookie);
        }   

        public void AddRookie(RookieInputDto rookie)
        {
            _rookiesRepository.AddRookie(rookie);
        }

        public List<RookieOutputDto> GetMales()
        {
            List<RookieOutputDto> maleRookies = _rookiesRepository.GetMales();

            return maleRookies;
        }

        public RookieOutputDto GetOldestRookie()
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetAllRookies();

            RookieOutputDto oldest = rookies.OrderByDescending(p => p.DateOfBirth).Last();

            return oldest;
        }

        public List<RookieOutputDto> GetAllRookies()
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetAllRookies();

            return rookies;
        }

        public List<RookieOutputDto> GetRookiesBornIn(int year)
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetRookiesBornIn(year);

            return rookies;
        }

        public List<RookieOutputDto> GetRookiesBornAfter(int year)
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetRookiesBornAfter(year);

            return rookies;
        }

        public List<RookieOutputDto> GetRookiesBornBefore(int year)
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetRookiesBornBefore(year);

            return rookies;
        }

        public Stream GetExcel()
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetAllRookies();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Rookies");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "First Name";
            worksheet.Cell("C1").Value = "Last Name";
            worksheet.Cell("D1").Value = "Gender";
            worksheet.Cell("E1").Value = "Date of Birth";
            worksheet.Cell("F1").Value = "Phone Number";
            worksheet.Cell("G1").Value = "Birth Place";
            worksheet.Cell("H1").Value = "Is Graduated";

            worksheet.Cell("A2").InsertData(rookies);

            Stream stream = new MemoryStream();

            workbook.SaveAs(stream);

            stream.Position = 0;

            return stream;
        }
    }
}
