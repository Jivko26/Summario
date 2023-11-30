using Microsoft.AspNetCore.Mvc;
using Moq;
using Summario.API.Controllers;
using Summario.Services.Interfaces;

namespace Summario.Testing
{
    public class TranslationControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WithTranslatedText()
        {
            // Arrange
            var mockTranslationService = new Mock<ITranslationService>();
            var testSummary = "test summary";
            var expectedResult = "translated text";

            mockTranslationService.Setup(t => t.Translate(testSummary))
                                  .ReturnsAsync(expectedResult);

            var controller = new TranslationController(mockTranslationService.Object);

            // Act
            var result = await controller.Get(testSummary);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<string>(okResult.Value);
            Assert.Equal(expectedResult, returnedResult);
        }

        [Fact]
        public async Task Get_ReturnsBadRequest_WhenSummaryIsNull()
        {
            // Arrange
            var mockTranslationService = new Mock<ITranslationService>();
            string testSummary = null;

            var controller = new TranslationController(mockTranslationService.Object);

            // Act
            var result = await controller.Get(testSummary);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }


    }
}
