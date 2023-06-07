namespace WebApi.Tests.DoughController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("DoughTestsCollection")]
    public class DoughControllerDeleteTests
    {
        private readonly DoughControllerFixture _fixture;

        public DoughControllerDeleteTests(DoughControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgument_NoContent()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            NoContentResult expected = new NoContentResult();

            var resultOfCreating = _fixture.DoughsController.Insert(testDough);
            var successResult = resultOfCreating.Result as CreatedResult;
            var resultOfCreatingDough = successResult.Value as DoughDto;

            // Act
            var result = _fixture.DoughsController.Delete(resultOfCreatingDough.Id);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.Equal(expected.ToString(), noContentResult.ToString());
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgumentOfNonExistingDough_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.DoughsController.Delete("fdvjbkjdhfbv");
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
