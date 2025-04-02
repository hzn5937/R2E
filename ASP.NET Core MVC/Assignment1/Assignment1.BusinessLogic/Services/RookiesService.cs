using Assignment1.BusinessLogic.Interfaces;
using Assignment1.Data.Interfaces;
using Assignment1.Data.Models;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Assignment1.BusinessLogic.Services
{
    public class RookiesService : IRookiesService
    {
        private readonly IRookiesRepository _rookiesRepository;
        private readonly ILogger<RookiesService> _logger;

        public RookiesService(IRookiesRepository rookiesRepository, ILogger<RookiesService> logger)
        {
            _rookiesRepository = rookiesRepository;
            _logger = logger;
        }
        public string GetMales()
        {
            try
            {
                List<Person> maleRookies = _rookiesRepository.GetMales();

                if (!maleRookies.Any())
                {
                    return "No one is Male.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(Environment.NewLine, maleRookies.Select(p => p.ToString())));

                return sb.ToString();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public string GetOldestRookie()
        {
            try
            {
                List<Person> rookies = _rookiesRepository.GetAllRookies();

                Person oldest = rookies.OrderByDescending(p => p.DateOfBirth).Last();

                return oldest.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public string GetFullnames()
        {
            try
            {
                List<Person> rookies = _rookiesRepository.GetAllRookies();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(Environment.NewLine, rookies.Select(p => p.FullName())));

                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public string GetRookiesBornIn(int year)
        {
            try
            {
                List<Person> rookies = _rookiesRepository.GetRookiesBornIn(year);

                if (!rookies.Any())
                {
                    return $"No one was born in {year}.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(Environment.NewLine, rookies.Select(p => p.ToString())));

                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public string GetRookiesBornAfter(int year)
        {
            try
            {
                List<Person> rookies = _rookiesRepository.GetRookiesBornAfter(year);

                if (!rookies.Any())
                {
                    return $"No one was born after {year}.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(Environment.NewLine, rookies.Select(p => p.ToString())));

                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public string GetRookiesBornBefore(int year)
        {
            try
            {
                List<Person> rookies = _rookiesRepository.GetRookiesBornBefore(year);

                if (!rookies.Any())
                {
                    return $"No one was born before {year}.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Join(Environment.NewLine, rookies.Select(p => p.ToString())));

                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw;
            }
        }

        public Stream GetExcel()
        {
            List<Person> rookies = _rookiesRepository.GetAllRookies();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Rookies");

            worksheet.Cell("A1").Value = "First Name";
            worksheet.Cell("B1").Value = "Last Name";
            worksheet.Cell("C1").Value = "Gender";
            worksheet.Cell("D1").Value = "Date of Birth";
            worksheet.Cell("E1").Value = "Phone Number";
            worksheet.Cell("F1").Value = "Birth Place";
            worksheet.Cell("G1").Value = "Is Graduated";

            worksheet.Cell("A2").InsertData(rookies);

            Stream stream = new MemoryStream();

            workbook.SaveAs(stream);

            stream.Position = 0;

            return stream;
        }
    }
}
