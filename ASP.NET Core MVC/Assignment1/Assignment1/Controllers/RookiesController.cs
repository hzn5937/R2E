using Microsoft.AspNetCore.Mvc;
using Assignment1.BusinessLogic.Interfaces;
using System;
using System.Reflection.Metadata.Ecma335;
using ClosedXML.Excel;

namespace Assignment1.Controllers
{
    public class RookiesController : Controller
    {
        private readonly IRookiesService _rookiesService;

        public RookiesController(IRookiesService rookiesService)
        {
            _rookiesService = rookiesService;
        }

        public IActionResult Male()
        {
                var output = _rookiesService.GetMales();

                return Content(output);
        }

        public IActionResult Oldest()
        {
            var output = _rookiesService.GetOldestRookie();

            return Content(output);
        }

        public IActionResult Fullnames()
        {
           var output = _rookiesService.GetFullnames();
            return Content(output);
        }

        public IActionResult FilterByBirthYear(string filter, int? year=2000)
        {
            string redirectActionName = filter?.ToLower() switch
            {
                "equal" => nameof(BornIn),
                "after" => nameof(BornAfter),
                "before" => nameof(BornBefore),
                _ => "error",
            };

            if (redirectActionName == "error")
            {
                return Content("Invalid filter, try using 'equal', 'before', and 'after'");
            }

            return RedirectToAction(redirectActionName, new { year });
        }

        public IActionResult BornIn(int year)
        {
            var output = _rookiesService.GetRookiesBornIn(year);
            return Content(output);
        }

        public IActionResult BornAfter(int year)
        {
            var output = _rookiesService.GetRookiesBornAfter(year);

            return Content(output);
        }

        public IActionResult BornBefore(int year)
        {
            var output = _rookiesService.GetRookiesBornBefore(year);

            return Content(output);
        }

        public IActionResult Excel()
        {
            Stream stream = _rookiesService.GetExcel();

            return File(stream, "application/octet-stream", "Rookies.xlsx");
        }
    }
}
