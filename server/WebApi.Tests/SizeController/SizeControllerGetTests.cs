namespace WebApi.Tests.SizeController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("SizeTestsCollection")]
    public class SizeControllerGetTests
    {
        private readonly SizeControllerFixture _fixture;

        public SizeControllerGetTests(SizeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Get_IdentificatorIntegerArgument_SizeDto()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var expectedSize = new SizeDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            // Act
            var insertingResult = _fixture.SizesController.Insert(testSize);
            var successedResult = insertingResult.Result as CreatedResult;
            var inBaseSize = successedResult.Value as SizeDto;

            var result = _fixture.SizesController.Get(inBaseSize.Id);
            var successResult = result.Result as OkObjectResult;
            var receivedSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(receivedSize, expectedSize));

            // Clear changes
            _fixture.SizesController.Delete(inBaseSize.Id);
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingSize_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.SizesController.Get("Non existent");
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
