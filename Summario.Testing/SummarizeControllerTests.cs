using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Summario.Services.Interfaces;

namespace Summario.Testing
{
    public class SummarizerControllerTests
    {
        [Fact]
        public async Task SummarizeArticle_ReturnsOkResults_WithSummaries()
        {
            // Arrange
            var mockSummarizerService = new Mock<IGptSummarizerService>();
            var mockPdfService = new Mock<IPdfService>();
            var testFile = new Mock<IFormFile>();
            var testText = "extracted text from PDF";
            var expectedResult = new List<string> { "summary1", "summary2" };

            mockPdfService.Setup(p => p.ExtractTextFromPdfAsync(It.IsAny<IFormFile>()))
                          .ReturnsAsync(testText);
            mockSummarizerService.Setup(s => s.SummarizeAsync(It.IsAny<string>()))
                                 .ReturnsAsync(expectedResult);

            var controller = new SummarizerController(mockSummarizerService.Object, mockPdfService.Object);

            // Act
            var result = await controller.SummarizeArtilce(testFile.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<List<string>>(okResult.StatusCode);
            Assert.Equal(expectedResult, returnedResult);
        }

        [Fact]
        public async Task SummarizeArticle_ReturnsBadResult_WithEmptyFile()
        {
            // Arrange
            var mockSummarizerService = new Mock<IGptSummarizerService>();
            var mockPdfService = new Mock<IPdfService>();
            var testFile = new Mock<IFormFile>();
            var testText = "";
            var expectedResult = new List<string> { "summary1", "summary2" };

            mockPdfService.Setup(p => p.ExtractTextFromPdfAsync(It.IsAny<IFormFile>()))
                          .ReturnsAsync(testText);
            mockSummarizerService.Setup(s => s.SummarizeAsync(It.IsAny<string>()))
                                 .ReturnsAsync(expectedResult);

            var controller = new SummarizerController(mockSummarizerService.Object, mockPdfService.Object);

            // Act
            var result = await controller.SummarizeArtilce(testFile.Object);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnedResult = Assert.IsType<List<string>>(badResult.Value);
            Assert.Equal(expectedResult, returnedResult);
        }
    }

}
