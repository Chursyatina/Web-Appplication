namespace WebApi.Tests.SizeController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("SizeTestsCollection")]
    public class SizeControllerDeleteTests
    {
        private readonly SizeControllerFixture _fixture;

        public SizeControllerDeleteTests(SizeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgument_NoContent()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "TestSize",
                PriceMultiplier = 7,
            };

            NoContentResult expected = new NoContentResult();

            var resultOfCreating = _fixture.SizesController.Insert(testSize);
            var successResult = resultOfCreating.Result as CreatedResult;
            var resultOfCreatingSize = successResult.Value as SizeDto;

            // Act
            var result = _fixture.SizesController.Delete(resultOfCreatingSize.Id);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.Equal(expected.ToString(), noContentResult.ToString());
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgumentOfNonExistingSize_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.SizesController.Delete("Non existent");
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
