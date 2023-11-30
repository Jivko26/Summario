using Microsoft.AspNetCore.Mvc;
using Moq;
using Summario.API.Controllers;
using Summario.Services.Interfaces;

namespace Summario.Testing
{
    public class SearchControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WithSearchResult()
        {
            // Arrange
            var mockSearchService = new Mock<ISearchService>();
            var testQuery = "test";
            var expectedResult = "some search result string";
            mockSearchService.Setup(s => s.SearchAsync(testQuery))
                             .ReturnsAsync(expectedResult);

            var controller = new SearchController(mockSearchService.Object);

            // Act
            var result = await controller.Get(testQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<string>(okResult.Value);
            Assert.Equal(expectedResult, returnedResult);
        }

    }

}