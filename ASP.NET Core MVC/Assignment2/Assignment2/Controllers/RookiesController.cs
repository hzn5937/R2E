using Microsoft.AspNetCore.Mvc;
using Assignment2.BusinessLogic.Interfaces;
using Assignment2.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment2.Model.RookieDto;
using Assignment2.BusinessLogic.Services;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Assignment2.Controllers
{
    public class RookiesController : Controller
    {
        private readonly IRookiesService _rookiesService;
        private readonly ILogger<RookiesService> _logger;


        public RookiesController(IRookiesService rookiesService, ILogger<RookiesService> logger)
        {
            _rookiesService = rookiesService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Rookies/All")]
        public IActionResult ViewAllRookies(int pageNum=1)
        {
            try
            {
                PaginatedRookieOutputDto paginatedRookies = _rookiesService.GetPaginatedRookies(pageNum);

                return View(paginatedRookies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting all rookies");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Delete(int rookieId)
        {
            try
            {
                _rookiesService.DeleteRookie(rookieId);
                TempData["DeleteSuccessMsg"] = "Deleted successfully";
                return RedirectToAction(nameof(ViewAllRookies));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting rookie");
                return RedirectToAction(nameof(ViewAllRookies));
            }
        }

        public IActionResult Details(int rookieId)
        {
            try
            {
                RookieOutputDto rookie = _rookiesService.GetRookieById(rookieId);
                return View(rookie);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rookie details");
                return RedirectToAction(nameof(ViewAllRookies));
            }
        }

        public IActionResult Edit(int rookieId)
        {
            try
            {
                RookieOutputDto rookie = _rookiesService.GetRookieById(rookieId);

                RookieInputDto rookieInputDto = new RookieInputDto()
                {
                    Id = rookie.Id,
                    FirstName = rookie.FirstName,
                    LastName = rookie.LastName,
                    Gender = rookie.Gender,
                    DateOfBirth = rookie.DateOfBirth,
                    PhoneNumber = rookie.PhoneNumber,
                    BirthPlace = rookie.BirthPlace,
                    IsGraduated = rookie.IsGraduated
                };

                var genderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                };

                ViewBag.GenderList = genderOptions;

                return View(rookieInputDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rookie details");
                return RedirectToAction(nameof(ViewAllRookies));
            }
        }

        [HttpPost]
        public IActionResult Edit(RookieInputDto rookie)
        {
            try
            {
                _rookiesService.UpdateRookie(rookie);
                return RedirectToAction(nameof(ViewAllRookies));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating rookie");
                return RedirectToAction(nameof(ViewAllRookies));
            }
        }

        public IActionResult Create()
        {
            try
            {
                var genderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                };

                ViewBag.GenderList = genderOptions;

                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating rookie");
                return RedirectToAction(nameof(ViewAllRookies));
            }
            
        }

        [HttpPost]
        public IActionResult Create(RookieInputDto inputDto)
        {
            try
            {
                if (inputDto.DateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                {
                    ModelState.AddModelError("DateOfBirth", "Date of birth cannot be in the future.");
                }

                if (!ModelState.IsValid)
                {
                    var genderOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Male", Text = "Male" },
                        new SelectListItem { Value = "Female", Text = "Female" },
                    };

                    ViewBag.GenderList = genderOptions;
                    return View(inputDto);
                }

                _rookiesService.AddRookie(inputDto);
                return RedirectToAction(nameof(ViewAllRookies));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating rookie");
                return RedirectToAction(nameof(ViewAllRookies));
            }
        }

        [HttpGet("Rookies/Males")]
        public IActionResult GetMaleRookies()
        {
            try
            {
                List<RookieOutputDto> maleRookies = _rookiesService.GetMales();

                return View(maleRookies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting male rookies");

                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpGet("Rookies/Oldest")]
        public IActionResult GetOldest()
        {
            try
            {
                RookieOutputDto oldestRookie = _rookiesService.GetOldestRookie();

                return View(oldestRookie);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting oldest rookie");

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("Rookies/Fullnames")]
        public IActionResult GetFullnames()
        {
            try
            {
                List<RookieOutputDto> rookies = _rookiesService.GetAllRookies();

                return View(rookies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting fullnames");

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("Rookies/FilterByBirthYear")]
        public IActionResult FilterByBirthYear(string filter, int? year = 2000)
        {
            try
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
                    return View();
                }

                return RedirectToAction(redirectActionName, new { year });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error filtering by birth year");

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult BornIn(int year)
        {
            try
            {
                List<RookieOutputDto> rookies = _rookiesService.GetRookiesBornIn(year);
                ViewBag.Year = year;
                return View(rookies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rookies born in year");

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult BornAfter(int year)
        {
            try
            {
                List<RookieOutputDto> rookies = _rookiesService.GetRookiesBornAfter(year);
                ViewBag.Year = year;
                return View(rookies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rookies born after year");

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult BornBefore(int year)
        {
            try
            {
                List<RookieOutputDto> rookies = _rookiesService.GetRookiesBornBefore(year);
                ViewBag.Year = year;
                return View(rookies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rookies born before year");

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult GetExcel()
        {
            try
            {
                Stream stream = _rookiesService.GetExcel();

                return File(stream, "application/octet-stream", "Rookies.xlsx");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting excel");

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
