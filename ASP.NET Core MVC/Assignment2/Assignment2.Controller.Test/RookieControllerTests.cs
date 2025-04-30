using Assignment2.BusinessLogic.Interfaces;
using Assignment2.BusinessLogic.Services;
using Assignment2.Controllers;
using Assignment2.Model;
using Assignment2.Model.RookieDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Moq;

namespace Assignment2.Web.Test
{
    [TestFixture]
    public class RookiesControllerTests
    {
        private Mock<IRookiesService> _mockRookiesService;
        private RookiesController _rookiesController;
        private Mock<ILogger<RookiesService>> _logger;

        [SetUp]
        public void Setup()
        {
            _mockRookiesService = new Mock<IRookiesService>();
            _logger = new Mock<ILogger<RookiesService>>();
            _rookiesController = new RookiesController(_mockRookiesService.Object, _logger.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _rookiesController?.Dispose();
        }

        // Helper function
        private void VerifyLogError<TException>(Times times) where TException : Exception
        {
            _logger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true), // Match any message state
                    It.IsAny<TException>(), // Match the specific exception type
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), // Match any formatter
                times);
        }

        // Methods/Actions to be tested
        [Test]
        public void Index_Always_ReturnsViewResult()
        {
            // Act
            var result = _rookiesController.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void ViewAllRookies_ServiceReturnsData_ReturnsViewResultWithModel()
        {
            // Arrange
            int pageNum = 1;
            var expectedModel = new PaginatedRookieOutputDto { Rookies = new List<RookieOutputDto>(), PageNumber = pageNum };
            _mockRookiesService.Setup(s => s.GetPaginatedRookies(pageNum)).Returns(expectedModel);

            // Act
            var result = _rookiesController.ViewAllRookies(pageNum) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PaginatedRookieOutputDto>(result.Model);
            Assert.AreEqual(expectedModel, result.Model);
            _mockRookiesService.Verify(s => s.GetPaginatedRookies(pageNum), Times.Once);
        }

        [Test]
        public void ViewAllRookies_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            int pageNum = 1;
            var exception = new InvalidOperationException("Service failure");
            _mockRookiesService.Setup(s => s.GetPaginatedRookies(pageNum)).Throws(exception);

            // Act
            var result = _rookiesController.ViewAllRookies(pageNum) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<InvalidOperationException>(Times.Once());
            _mockRookiesService.Verify(s => s.GetPaginatedRookies(pageNum), Times.Once);
        }

        [Test]
        public void Delete_ServiceSucceeds_SetsTempDataAndRedirectsToViewAll()
        {
            // Arrange
            int rookieId = 5;
            _mockRookiesService.Setup(s => s.DeleteRookie(rookieId));

            // Act
            var result = _rookiesController.Delete(rookieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            _mockRookiesService.Verify(s => s.DeleteRookie(rookieId), Times.Once);
        }

        [Test]
        public void Delete_ServiceThrowsException_LogsErrorAndRedirectsToViewAll()
        {
            // Arrange
            int rookieId = 5;
            var exception = new KeyNotFoundException("Rookie not found");
            _mockRookiesService.Setup(s => s.DeleteRookie(rookieId)).Throws(exception);

            // Act
            var result = _rookiesController.Delete(rookieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            VerifyLogError<KeyNotFoundException>(Times.Once());
            _mockRookiesService.Verify(s => s.DeleteRookie(rookieId), Times.Once);
        }

        [Test]
        public void Details_ServiceReturnsData_ReturnsViewResultWithModel()
        {
            // Arrange
            int rookieId = 1;
            var expectedModel = new RookieOutputDto { Id = rookieId, FirstName = "Test" };
            _mockRookiesService.Setup(s => s.GetRookieById<RookieOutputDto>(rookieId)).Returns(expectedModel);

            // Act
            var result = _rookiesController.Details(rookieId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RookieOutputDto>(result.Model);
            Assert.AreEqual(expectedModel, result.Model);
            _mockRookiesService.Verify(s => s.GetRookieById<RookieOutputDto>(rookieId), Times.Once);
        }

        [Test]
        public void Details_ServiceReturnsNull_ReturnsViewResultWithNullModel()
        {
            // Arrange
            int rookieId = 99;
            _mockRookiesService.Setup(s => s.GetRookieById<RookieOutputDto>(rookieId)).Returns((RookieOutputDto)null);

            // Act
            var result = _rookiesController.Details(rookieId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
            _mockRookiesService.Verify(s => s.GetRookieById<RookieOutputDto>(rookieId), Times.Once);
        }

        [Test]
        public void Details_ServiceThrowsException_LogsErrorAndRedirectsToViewAll()
        {
            // Arrange
            int rookieId = 1;
            var exception = new Exception("Database error");
            _mockRookiesService.Setup(s => s.GetRookieById<RookieOutputDto>(rookieId)).Throws(exception);

            // Act
            var result = _rookiesController.Details(rookieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetRookieById<RookieOutputDto>(rookieId), Times.Once);
        }

        [Test]
        public void EditGet_ServiceReturnsData_ReturnsViewResultWithModelAndViewBag()
        {
            // Arrange
            int rookieId = 2;
            var expectedModel = new RookieInputDto { Id = rookieId, FirstName = "Test Edit" };
            _mockRookiesService.Setup(s => s.GetRookieById<RookieInputDto>(rookieId)).Returns(expectedModel);

            // Act
            var result = _rookiesController.Edit(rookieId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RookieInputDto>(result.Model);
            Assert.AreEqual(expectedModel, result.Model);
            Assert.IsNotNull(result.ViewData["GenderList"]);
            Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["GenderList"]);
            _mockRookiesService.Verify(s => s.GetRookieById<RookieInputDto>(rookieId), Times.Once);
        }

        [Test]
        public void EditGet_ServiceReturnsNull_ReturnsViewResultWithNullModelAndViewBag()
        {
            // Arrange
            int rookieId = 99;
            _mockRookiesService.Setup(s => s.GetRookieById<RookieInputDto>(rookieId)).Returns((RookieInputDto)null);

            // Act
            var result = _rookiesController.Edit(rookieId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
            Assert.IsNotNull(result.ViewData["GenderList"]);
            Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["GenderList"]);
            _mockRookiesService.Verify(s => s.GetRookieById<RookieInputDto>(rookieId), Times.Once);
        }

        [Test]
        public void EditGet_ServiceThrowsException_LogsErrorAndRedirectsToViewAll()
        {
            // Arrange
            int rookieId = 1;
            var exception = new Exception("Fetch error");
            _mockRookiesService.Setup(s => s.GetRookieById<RookieInputDto>(rookieId)).Throws(exception);

            // Act
            var result = _rookiesController.Edit(rookieId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetRookieById<RookieInputDto>(rookieId), Times.Once);
        }

        [Test]
        public void EditPost_ServiceSucceeds_RedirectsToViewAll()
        {
            // Arrange
            var rookieToUpdate = new RookieInputDto { Id = 1, FirstName = "Updated" };
            _mockRookiesService.Setup(s => s.UpdateRookie(rookieToUpdate)); // Setup void method

            // Act
            var result = _rookiesController.Edit(rookieToUpdate) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            _mockRookiesService.Verify(s => s.UpdateRookie(rookieToUpdate), Times.Once);
        }

        [Test]
        public void EditPost_ServiceThrowsException_LogsErrorAndRedirectsToViewAll()
        {
            // Arrange
            var rookieToUpdate = new RookieInputDto { Id = 1, FirstName = "Updated" };
            var exception = new Exception("Update failed");
            _mockRookiesService.Setup(s => s.UpdateRookie(rookieToUpdate)).Throws(exception);

            // Act
            var result = _rookiesController.Edit(rookieToUpdate) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.UpdateRookie(rookieToUpdate), Times.Once);
        }

        [Test]
        public void CreateGet_Always_ReturnsViewResultWithViewBag()
        {
            // Arrange

            // Act
            var result = _rookiesController.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
            Assert.IsNotNull(result.ViewData["GenderList"]);
            Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["GenderList"]);
        }

        [Test]
        public void CreatePost_ModelStateValid_CallsServiceAndRedirectsToViewAll()
        {
            // Arrange
            var validInput = new RookieInputDto { FirstName = "New", LastName = "Rookie", DateOfBirth = new DateOnly(2000, 1, 1) };
            _mockRookiesService.Setup(s => s.AddRookie(validInput));

            // Act
            var result = _rookiesController.Create(validInput) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            _mockRookiesService.Verify(s => s.AddRookie(validInput), Times.Once);
        }

        [Test]
        public void CreatePost_InvalidModelState_FutureDob_ReturnsViewResultWithModelAndViewBag()
        {
            // Arrange
            var invalidInput = new RookieInputDto { FirstName = "Future", LastName = "Person", DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(1)) };
            // Manually add the expected model error (matches controller logic)
            _rookiesController.ModelState.AddModelError("DateOfBirth", "Date of birth cannot be in the future.");

            // Act
            var result = _rookiesController.Create(invalidInput) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_rookiesController.ModelState.IsValid); 
            Assert.AreEqual(invalidInput, result.Model); 
            Assert.IsNotNull(result.ViewData["GenderList"]);
            Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["GenderList"]);
            _mockRookiesService.Verify(s => s.AddRookie(It.IsAny<RookieInputDto>()), Times.Never);
        }

        [Test]
        public void CreatePost_AnotherInvalidModelState_ReturnsViewResultWithModelAndViewBag()
        {
            // Arrange
            var invalidInput = new RookieInputDto { DateOfBirth = new DateOnly(2000, 1, 1) };
            // Manually add a different model error
            _rookiesController.ModelState.AddModelError("FirstName", "First name is required");

            // Act
            var result = _rookiesController.Create(invalidInput) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_rookiesController.ModelState.IsValid); 
            Assert.AreEqual(invalidInput, result.Model);
            Assert.IsNotNull(result.ViewData["GenderList"]);
            Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["GenderList"]);
            _mockRookiesService.Verify(s => s.AddRookie(It.IsAny<RookieInputDto>()), Times.Never); 
        }

        [Test]
        public void CreatePost_ServiceThrowsException_LogsErrorAndRedirectsToViewAll()
        {
            // Arrange
            var validInput = new RookieInputDto { FirstName = "New", LastName = "Rookie", DateOfBirth = new DateOnly(2000, 1, 1) };
            var exception = new Exception("Add failed");
            _mockRookiesService.Setup(s => s.AddRookie(validInput)).Throws(exception);

            // Act
            var result = _rookiesController.Create(validInput) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.ViewAllRookies), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.AddRookie(validInput), Times.Once); // Verify attempt was made
        }

        [Test]
        public void GetMaleRookies_ServiceReturnsData_ReturnsViewResultWithModel()
        {
            // Arrange
            var expectedModel = new List<RookieOutputDto> { new RookieOutputDto { Gender = Gender.Male } };
            _mockRookiesService.Setup(s => s.GetMales()).Returns(expectedModel);

            // Act
            var result = _rookiesController.GetMaleRookies() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<RookieOutputDto>>(result.Model);
            Assert.AreEqual(expectedModel, result.Model);
            _mockRookiesService.Verify(s => s.GetMales(), Times.Once);
        }

        [Test]
        public void GetMaleRookies_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            var exception = new Exception("GetMales failed");
            _mockRookiesService.Setup(s => s.GetMales()).Throws(exception);

            // Act
            var result = _rookiesController.GetMaleRookies() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetMales(), Times.Once);
        }

        [Test]
        public void GetOldest_ServiceReturnsData_ReturnsViewResultWithModel()
        {
            // Arrange
            var expectedModel = new RookieOutputDto { Id = 99, FirstName = "Oldest" };
            _mockRookiesService.Setup(s => s.GetOldestRookie()).Returns(expectedModel);

            // Act
            var result = _rookiesController.GetOldest() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RookieOutputDto>(result.Model);
            Assert.AreEqual(expectedModel, result.Model);
            _mockRookiesService.Verify(s => s.GetOldestRookie(), Times.Once);
        }

        [Test]
        public void GetOldest_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            var exception = new InvalidOperationException("Sequence contains no elements");
            _mockRookiesService.Setup(s => s.GetOldestRookie()).Throws(exception);

            // Act
            var result = _rookiesController.GetOldest() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<InvalidOperationException>(Times.Once());
            _mockRookiesService.Verify(s => s.GetOldestRookie(), Times.Once);
        }

        [Test]
        public void GetFullnames_ServiceReturnsData_ReturnsViewResultWithModel()
        {
            // Arrange
            var expectedModel = new List<RookieOutputDto> { new RookieOutputDto { Id = 1 } };
            _mockRookiesService.Setup(s => s.GetAllRookies()).Returns(expectedModel);

            // Act
            var result = _rookiesController.GetFullnames() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<RookieOutputDto>>(result.Model);
            Assert.AreEqual(expectedModel, result.Model);
            _mockRookiesService.Verify(s => s.GetAllRookies(), Times.Once);
        }

        [Test]
        public void GetFullnames_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            var exception = new Exception("GetAll failed");
            _mockRookiesService.Setup(s => s.GetAllRookies()).Throws(exception);

            // Act
            var result = _rookiesController.GetFullnames() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetAllRookies(), Times.Once);
        }


        [TestCase("equal", nameof(RookiesController.BornIn))]
        [TestCase("after", nameof(RookiesController.BornAfter))]
        [TestCase("before", nameof(RookiesController.BornBefore))]
        [TestCase("EQUAL", nameof(RookiesController.BornIn))]
        public void FilterByBirthYear_ValidFilter_RedirectsToCorrectAction(string filter, string expectedAction)
        {
            // Arrange
            int year = 2001;

            // Act
            var result = _rookiesController.FilterByBirthYear(filter, year) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAction, result.ActionName);
            Assert.IsTrue(result.RouteValues.ContainsKey("year"));
            Assert.AreEqual(year, result.RouteValues["year"]);
        }

        [TestCase("invalid")]
        [TestCase("")]
        public void FilterByBirthYear_InvalidFilter_ReturnsDefaultView(string filter)
        {
            // Arrange
            int year = 2001;

            // Act
            var result = _rookiesController.FilterByBirthYear(filter, year) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
        }

        [Test]
        public void BornIn_ServiceReturnsData_ReturnsViewResultWithModelAndViewBag()
        {
            // Arrange
            int year = 2000;
            var expectedModel = new List<RookieOutputDto>();
            _mockRookiesService.Setup(s => s.GetRookiesBornIn(year)).Returns(expectedModel);

            // Act
            var result = _rookiesController.BornIn(year) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedModel, result.Model);
            Assert.AreEqual(year, result.ViewData["Year"]);
            _mockRookiesService.Verify(s => s.GetRookiesBornIn(year), Times.Once);
        }

        [Test]
        public void BornIn_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            int year = 2000;
            var exception = new Exception("BornIn search failed");
            _mockRookiesService.Setup(s => s.GetRookiesBornIn(year)).Throws(exception);

            // Act
            var result = _rookiesController.BornIn(year) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetRookiesBornIn(year), Times.Once);
        }

        [Test]
        public void GetExcel_ServiceReturnsStream_ReturnsFileStreamResult()
        {
            // Arrange
            var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 }); // Sample stream
            _mockRookiesService.Setup(s => s.GetExcel()).Returns(memoryStream);

            // Act
            var result = _rookiesController.GetExcel() as FileStreamResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("application/octet-stream", result.ContentType);
            Assert.AreEqual("Rookies.xlsx", result.FileDownloadName);
            Assert.AreEqual(memoryStream, result.FileStream);
            _mockRookiesService.Verify(s => s.GetExcel(), Times.Once);

            memoryStream.Dispose();
        }

        [Test]
        public void GetExcel_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            var exception = new Exception("Excel generation failed");
            _mockRookiesService.Setup(s => s.GetExcel()).Throws(exception);

            // Act
            var result = _rookiesController.GetExcel() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetExcel(), Times.Once);
        }

        [Test]
        public void BornAfter_ServiceReturnsData_ReturnsViewResultWithModelAndViewBag()
        {
            // Arrange
            int year = 2000;
            var expectedModel = new List<RookieOutputDto>();
            _mockRookiesService.Setup(s => s.GetRookiesBornAfter(year)).Returns(expectedModel);
            // Act
            var result = _rookiesController.BornAfter(year) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedModel, result.Model);
            Assert.AreEqual(year, result.ViewData["Year"]);
            _mockRookiesService.Verify(s => s.GetRookiesBornAfter(year), Times.Once);
        }

        [Test]
        public void BornAfter_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            int year = 2000;
            var exception = new Exception("BornAfter search failed");
            _mockRookiesService.Setup(s => s.GetRookiesBornAfter(year)).Throws(exception);
            // Act
            var result = _rookiesController.BornAfter(year) as RedirectToActionResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetRookiesBornAfter(year), Times.Once);
        }

        [Test]
        public void BornBefore_ServiceReturnsData_ReturnsViewResultWithModelAndViewBag()
        {
            // Arrange
            int year = 2000;
            var expectedModel = new List<RookieOutputDto>();
            _mockRookiesService.Setup(s => s.GetRookiesBornBefore(year)).Returns(expectedModel);
            // Act
            var result = _rookiesController.BornBefore(year) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedModel, result.Model);
            Assert.AreEqual(year, result.ViewData["Year"]);
            _mockRookiesService.Verify(s => s.GetRookiesBornBefore(year), Times.Once);
        }

        [Test]
        public void BornBefore_ServiceThrowsException_LogsErrorAndRedirectsToIndex()
        {
            // Arrange
            int year = 2000;
            var exception = new Exception("BornBefore search failed");
            _mockRookiesService.Setup(s => s.GetRookiesBornBefore(year)).Throws(exception);
            // Act
            var result = _rookiesController.BornBefore(year) as RedirectToActionResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(RookiesController.Index), result.ActionName);
            VerifyLogError<Exception>(Times.Once());
            _mockRookiesService.Verify(s => s.GetRookiesBornBefore(year), Times.Once);
        }
    }
}
