using Assignment2.BusinessLogic.Interfaces;
using Assignment2.BusinessLogic.Services;
using Assignment2.Data.Interfaces;
using Assignment2.Model.RookieDto;
using Assignment2.Model;
using Moq;
using Assignment2.Data.Models;

namespace Assignment2.BusinessLogic.Test
{
    [TestFixture]
    public class RookiesServiceTests
    {
        private Mock<IRookiesRepository> _mockRookiesRepository;
        private IRookiesService _rookiesService;

        [SetUp]
        public void Setup()
        {
            _mockRookiesRepository = new Mock<IRookiesRepository>();
            _rookiesService = new RookiesService(_mockRookiesRepository.Object);
        }

        // Helper method to create dummy data
        private List<RookieOutputDto> GetTestRookies()
        {
            return new List<RookieOutputDto>
            {
                new RookieOutputDto { Id = 1, FirstName = "John", LastName = "Doe", Gender = Gender.Male, DateOfBirth = new DateOnly(2000, 1, 1), PhoneNumber = "1234567890", BirthPlace = "City A", IsGraduated = true },
                new RookieOutputDto { Id = 2, FirstName = "Jane", LastName = "Smith", Gender = Gender.Female, DateOfBirth = new DateOnly(2001, 5, 10), PhoneNumber = "0987654321", BirthPlace = "City B", IsGraduated = false },
                new RookieOutputDto { Id = 3, FirstName = "Peter", LastName = "Jones", Gender = Gender.Male, DateOfBirth = new DateOnly(1999, 11, 20), PhoneNumber = "1122334455", BirthPlace = "City C", IsGraduated = true },
                new RookieOutputDto { Id = 4, FirstName = "Alice", LastName = "Brown", Gender = Gender.Female, DateOfBirth = new DateOnly(2000, 3, 15), PhoneNumber = "5566778899", BirthPlace = "City A", IsGraduated = false },
                new RookieOutputDto { Id = 5, FirstName = "Bob", LastName = "White", Gender = Gender.Male, DateOfBirth = new DateOnly(2002, 7, 25), PhoneNumber = "9988776655", BirthPlace = "City D", IsGraduated = true },
                new RookieOutputDto { Id = 6, FirstName = "Charlie", LastName = "Green", Gender = Gender.Male, DateOfBirth = new DateOnly(2000, 9, 5), PhoneNumber = "6655443322", BirthPlace = "City B", IsGraduated = false },
            };
        }

        [Test]
        public void GetPaginatedRookies_Page1_ReturnsCorrectPage()
        {
            // Arrange
            var allRookies = GetTestRookies();
            int pageNum = 1;
            int pageSize = 5;
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(allRookies);

            // Act
            var result = _rookiesService.GetPaginatedRookies(pageNum);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pageSize, result.Rookies.Count);
            Assert.AreEqual(pageNum, result.PageNumber);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.AreEqual(allRookies.Count, 6); 
            Assert.AreEqual(2, result.TotalPage);
            Assert.IsTrue(result.HasNext);
            Assert.IsFalse(result.HasPrevious);
            Assert.AreEqual(1, result.Rookies.First().Id);
            Assert.AreEqual(5, result.Rookies.Last().Id);
        }

        [Test]
        public void GetPaginatedRookies_LastPage_ReturnsCorrectPage()
        {
            // Arrange
            var allRookies = GetTestRookies();
            int pageNum = 2;
            int pageSize = 5;
            int expectedCountOnLastPage = 1;
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(allRookies);


            // Act
            var result = _rookiesService.GetPaginatedRookies(pageNum);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCountOnLastPage, result.Rookies.Count);
            Assert.AreEqual(pageNum, result.PageNumber);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.AreEqual(2, result.TotalPage);
            Assert.IsFalse(result.HasNext);
            Assert.IsTrue(result.HasPrevious);
            Assert.AreEqual(6, result.Rookies.First().Id);
            Assert.AreNotEqual(7, result.Rookies.First().Id);
            Assert.IsNull(allRookies.FirstOrDefault(r => r.Id == 7));
        }

        [Test]
        public void GetPaginatedRookies_EmptyList_ReturnsEmptyResult()
        {
            // Arrange
            var allRookies = new List<RookieOutputDto>();
            int pageNum = 1;
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(allRookies);

            // Act
            var result = _rookiesService.GetPaginatedRookies(pageNum);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pageNum, result.PageNumber);
            Assert.AreEqual(5, result.PageSize); 
            Assert.AreEqual(0, result.TotalPage);
            Assert.IsEmpty(result.Rookies);
            Assert.IsFalse(result.HasNext);
            Assert.IsFalse(result.HasPrevious);
        }

        [Test]
        public void GetRookieById_AsOutputDto_ReturnsCorrectRookie()
        {
            // Arrange
            int rookieId = 2;
            var expectedRookie = GetTestRookies().First(r => r.Id == rookieId);
            _mockRookiesRepository.Setup(repo => repo.GetRookieById(rookieId)).Returns(expectedRookie);

            // Act
            var result = _rookiesService.GetRookieById<RookieOutputDto>(rookieId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RookieOutputDto>(result);
            Assert.AreEqual(expectedRookie.Id, result.Id);
            Assert.AreEqual(expectedRookie.FirstName, result.FirstName);
            Assert.AreEqual(expectedRookie.LastName, result.LastName);
            Assert.AreEqual(expectedRookie.Gender, result.Gender);
            Assert.AreEqual(expectedRookie.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(expectedRookie.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(expectedRookie.BirthPlace, result.BirthPlace);
            Assert.AreEqual(expectedRookie.IsGraduated, result.IsGraduated);
            Assert.AreSame(expectedRookie, result);
            _mockRookiesRepository.Verify(repo => repo.GetRookieById(rookieId), Times.Once);
        }

        [Test]
        public void GetRookieById_AsInputDto_ReturnsCorrectRookie()
        {
            // Arrange
            int rookieId = 1;
            var outputDto = GetTestRookies().First(r => r.Id == rookieId);
            _mockRookiesRepository.Setup(repo => repo.GetRookieById(rookieId)).Returns(outputDto);

            var expectedInputDto = new RookieInputDto
            {
                Id = outputDto.Id,
                FirstName = outputDto.FirstName,
                LastName = outputDto.LastName,
                Gender = outputDto.Gender,
                DateOfBirth = outputDto.DateOfBirth,
                PhoneNumber = outputDto.PhoneNumber,
                BirthPlace = outputDto.BirthPlace,
                IsGraduated = outputDto.IsGraduated
            };

            // Act
            var result = _rookiesService.GetRookieById<RookieInputDto>(rookieId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RookieInputDto>(result);
            Assert.AreEqual(expectedInputDto.Id, result.Id);
            Assert.AreEqual(expectedInputDto.FirstName, result.FirstName);
            Assert.AreEqual(expectedInputDto.LastName, result.LastName);
            Assert.AreEqual(expectedInputDto.Gender, result.Gender);
            Assert.AreEqual(expectedInputDto.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(expectedInputDto.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(expectedInputDto.BirthPlace, result.BirthPlace);
            Assert.AreEqual(expectedInputDto.IsGraduated, result.IsGraduated);
            _mockRookiesRepository.Verify(repo => repo.GetRookieById(rookieId), Times.Once);
        }

        [Test]
        public void GetRookieById_NotFound_ReturnsNull()
        {
            // Arrange
            int nonExistentId = 99;
            _mockRookiesRepository.Setup(repo => repo.GetRookieById(nonExistentId)).Returns((RookieOutputDto)null);

            // Act
            var result = _rookiesService.GetRookieById<RookieOutputDto>(nonExistentId);

            // Assert
            Assert.IsNull(result);
            _mockRookiesRepository.Verify(repo => repo.GetRookieById(nonExistentId), Times.Once);
        }

        [Test]
        public void GetRookieById_InvalidType_ThrowsException()
        {
            // Arrange
            int rookieId = 1;
            var outputDto = GetTestRookies().First(r => r.Id == rookieId);
            _mockRookiesRepository.Setup(repo => repo.GetRookieById(rookieId)).Returns(outputDto); // Setup needed even if not directly used

            // Act & Assert
            // Use a type that is not RookieInputDto or RookieOutputDto
            Assert.Throws<InvalidOperationException>(() => _rookiesService.GetRookieById<Person>(rookieId));
            _mockRookiesRepository.Verify(repo => repo.GetRookieById(rookieId), Times.Never); // Verify GetRookieById wasn't called because the type check fails first
        }

        [Test]
        public void DeleteRookie_CallsRepositoryDelete()
        {
            // Arrange
            int rookieIdToDelete = 3;
            _mockRookiesRepository.Setup(repo => repo.DeleteRookie(rookieIdToDelete));

            // Act
            _rookiesService.DeleteRookie(rookieIdToDelete);

            // Assert
            _mockRookiesRepository.Verify(repo => repo.DeleteRookie(rookieIdToDelete), Times.Once);
        }

        [Test]
        public void UpdateRookie_CallsRepositoryUpdate()
        {
            // Arrange
            var rookieToUpdate = new RookieInputDto
            {
                Id = 1,
                FirstName = "Updated John",
                LastName = "Updated Doe",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(2001, 1, 1),
                PhoneNumber = "123456789",
                BirthPlace = "City A Updated",
                IsGraduated = false
            };

            _mockRookiesRepository.Setup(repo => repo.UpdateRookie(It.IsAny<RookieInputDto>())); 

            // Act
            _rookiesService.UpdateRookie(rookieToUpdate);

            // Assert
            _mockRookiesRepository.Verify(repo => repo.UpdateRookie(It.Is<RookieInputDto>(r =>
                r.Id == rookieToUpdate.Id &&
                r.FirstName == rookieToUpdate.FirstName &&
                r.LastName == rookieToUpdate.LastName &&
                r.Gender == rookieToUpdate.Gender &&
                r.DateOfBirth == rookieToUpdate.DateOfBirth &&
                r.PhoneNumber == rookieToUpdate.PhoneNumber &&
                r.BirthPlace == rookieToUpdate.BirthPlace &&
                r.IsGraduated == rookieToUpdate.IsGraduated)), Times.Once);
        }

        [Test]
        public void AddRookie_CallsRepositoryAdd()
        {
            // Arrange
            var rookieToAdd = new RookieInputDto
            {
                // Id is usually generated by repo/db
                FirstName = "New",
                LastName = "Rookie",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(2003, 3, 3),
                PhoneNumber = "1110001110",
                BirthPlace = "New City",
                IsGraduated = false
            };
            _mockRookiesRepository.Setup(repo => repo.AddRookie(It.IsAny<RookieInputDto>())); // Setup the void method

            // Act
            _rookiesService.AddRookie(rookieToAdd);

            // Assert
            _mockRookiesRepository.Verify(repo => repo.AddRookie(It.Is<RookieInputDto>(r =>
                r.FirstName == rookieToAdd.FirstName &&
                r.LastName == rookieToAdd.LastName &&
                r.BirthPlace == rookieToAdd.BirthPlace)), Times.Once);
        }

        [Test]
        public void GetMales_ReturnsOnlyMales()
        {
            // Arrange
            var testData = GetTestRookies();
            var expectedMales = testData.Where(r => r.Gender == Gender.Male).ToList();
            _mockRookiesRepository.Setup(repo => repo.GetMales()).Returns(expectedMales);

            // Act
            var result = _rookiesService.GetMales();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMales.Count, result.Count);
            Assert.IsTrue(result.All(r => r.Gender == Gender.Male));
            _mockRookiesRepository.Verify(repo => repo.GetMales(), Times.Once);
        }

        [Test]
        public void GetOldestRookie_ReturnsCorrectRookie()
        {
            // Arrange
            var testData = GetTestRookies();
            // Oldest has the minimum DateOfBirth
            var expectedOldest = testData.OrderBy(p => p.DateOfBirth).First();
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(testData); // Service calls GetAllRookies

            // Act
            var result = _rookiesService.GetOldestRookie();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedOldest.Id, result.Id);
            Assert.AreEqual(expectedOldest.DateOfBirth, result.DateOfBirth);
            _mockRookiesRepository.Verify(repo => repo.GetAllRookies(), Times.Once);
        }

        [Test]
        public void GetOldestRookie_EmptyList_ThrowsException()
        {
            // Arrange
            var emptyList = new List<RookieOutputDto>();
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(emptyList);

            // Act & Assert
            // The service uses LINQ .Last() on OrderByDescending, equivalent to First() on OrderBy, throws on empty sequence
            Assert.Throws<InvalidOperationException>(() => _rookiesService.GetOldestRookie());
            _mockRookiesRepository.Verify(repo => repo.GetAllRookies(), Times.Once); // Verify it was called even though it threw later
        }

        [Test]
        public void GetAllRookies_ReturnsAll()
        {
            // Arrange
            var expectedRookies = GetTestRookies();
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(expectedRookies);

            // Act
            var result = _rookiesService.GetAllRookies();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRookies.Count, result.Count);
            CollectionAssert.AreEquivalent(expectedRookies, result); // Checks content regardless of order
            _mockRookiesRepository.Verify(repo => repo.GetAllRookies(), Times.Once);
        }

        [Test]
        public void GetRookiesBornIn_ReturnsCorrectRookies()
        {
            // Arrange
            int year = 2000;
            var testData = GetTestRookies();
            var expectedRookies = testData.Where(r => r.DateOfBirth.Year == year).ToList();
            _mockRookiesRepository.Setup(repo => repo.GetRookiesBornIn(year)).Returns(expectedRookies);

            // Act
            var result = _rookiesService.GetRookiesBornIn(year);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRookies.Count, result.Count);
            Assert.IsTrue(result.All(r => r.DateOfBirth.Year == year));
            _mockRookiesRepository.Verify(repo => repo.GetRookiesBornIn(year), Times.Once);
        }

        [Test]
        public void GetRookiesBornAfter_ReturnsCorrectRookies()
        {
            // Arrange
            int year = 2000;
            var testData = GetTestRookies();
            var expectedRookies = testData.Where(r => r.DateOfBirth.Year > year).ToList();
            _mockRookiesRepository.Setup(repo => repo.GetRookiesBornAfter(year)).Returns(expectedRookies);

            // Act
            var result = _rookiesService.GetRookiesBornAfter(year);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRookies.Count, result.Count);
            Assert.IsTrue(result.All(r => r.DateOfBirth.Year > year));
            _mockRookiesRepository.Verify(repo => repo.GetRookiesBornAfter(year), Times.Once);
        }

        [Test]
        public void GetRookiesBornBefore_ReturnsCorrectRookies()
        {
            // Arrange
            int year = 2000;
            var testData = GetTestRookies();
            var expectedRookies = testData.Where(r => r.DateOfBirth.Year < year).ToList();
            _mockRookiesRepository.Setup(repo => repo.GetRookiesBornBefore(year)).Returns(expectedRookies);

            // Act
            var result = _rookiesService.GetRookiesBornBefore(year);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRookies.Count, result.Count);
            Assert.IsTrue(result.All(r => r.DateOfBirth.Year < year));
            _mockRookiesRepository.Verify(repo => repo.GetRookiesBornBefore(year), Times.Once);
        }

        [Test]
        public void GetExcel_CallsRepositoryAndReturnsReadableStream()
        {
            // Arrange
            var testData = GetTestRookies();
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(testData); // GetExcel uses GetAllRookies

            // Act
            Stream result = null;
            try
            {
                result = _rookiesService.GetExcel();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.CanRead); // Check if stream is readable
                Assert.AreEqual(0, result.Position); // Stream position should be reset to 0 for reading
                Assert.Greater(result.Length, 0); // Check if stream has content
                _mockRookiesRepository.Verify(repo => repo.GetAllRookies(), Times.Once); // Verify dependency call

                // Note: Verifying the actual Excel content is more of an integration test
                // and requires reading the stream with an Excel library (like ClosedXML)
                // which adds complexity to a unit test. Checking if the stream is valid and non-empty is often sufficient here.
            }
            finally
            {
                // Ensure stream is disposed even if assertions fail
                result?.Dispose();
            }
        }

        [Test]
        public void GetExcel_EmptyData_ReturnsValidStream()
        {
            // Arrange
            var emptyData = new List<RookieOutputDto>();
            _mockRookiesRepository.Setup(repo => repo.GetAllRookies()).Returns(emptyData);

            // Act
            Stream result = null;
            try
            {
                result = _rookiesService.GetExcel();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.CanRead);
                Assert.AreEqual(0, result.Position);
                Assert.Greater(result.Length, 0); // Excel file will still have headers even with no data rows
                _mockRookiesRepository.Verify(repo => repo.GetAllRookies(), Times.Once);
            }
            finally
            {
                result?.Dispose();
            }
        }
    }
}
