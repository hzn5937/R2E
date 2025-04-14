using Assignment2.BusinessLogic.Interfaces;
using Assignment2.Data.Models;
using Assignment2.Data.Interfaces;
using ClosedXML.Excel;
using Assignment2.Model.RookieDto;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Assignment2.BusinessLogic.Services
{
    public class RookiesService : IRookiesService
    {
        private readonly IRookiesRepository _rookiesRepository;

        public RookiesService(IRookiesRepository rookiesRepository)
        {
            _rookiesRepository = rookiesRepository;
        }

        public PaginatedRookieOutputDto GetPaginatedRookies(int pageNum)
        {
            List<RookieOutputDto> rookies = _rookiesRepository.GetAllRookies();

            int pageSize = 5;

            int totalRookies = rookies.Count;

            var totalPages = (int)Math.Ceiling(totalRookies / (double)pageSize);

            List<RookieOutputDto> paginatedRookies = new List<RookieOutputDto>();

            //List<RookieOutputDto> paginatedRookies = rookies.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            if (pageNum < totalPages)
            {
                paginatedRookies = rookies.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                int takes = totalRookies - ((pageNum - 1) * pageSize);
                paginatedRookies = rookies.Skip((pageNum - 1) * pageSize).Take(takes).ToList();
            }

            PaginatedRookieOutputDto output = new PaginatedRookieOutputDto()
            {
                Rookies = paginatedRookies,
                PageSize = pageSize,
                PageNumber = pageNum,
                TotalPage = totalPages,
                HasNext = pageNum < totalPages,
                HasPrevious = pageNum > 1
            };

            return output;
        }

        public T? GetRookieById<T>(int id) where T : class
        {
            if (typeof(T) == typeof(RookieOutputDto))
            {
                return _rookiesRepository.GetRookieById(id) as T;
            }

            if (typeof(T) == typeof(RookieInputDto))
            {
                var output = _rookiesRepository.GetRookieById(id);

                var input = new RookieInputDto
                {
                    Id = output.Id,
                    FirstName = output.FirstName,
                    LastName = output.LastName,
                    Gender = output.Gender,
                    DateOfBirth = output.DateOfBirth,
                    PhoneNumber = output.PhoneNumber,
                    BirthPlace = output.BirthPlace,
                    IsGraduated = output.IsGraduated
                };

                return input as T;
            }

            throw new InvalidOperationException("Unsupported type requested");
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
