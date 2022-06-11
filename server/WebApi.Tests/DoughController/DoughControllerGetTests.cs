namespace WebApi.Tests.DoughController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("DoughTestsCollection")]
    public class DoughControllerGetTests
    {
        private readonly DoughControllerFixture _fixture;

        public DoughControllerGetTests(DoughControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Get_IdentificatorIntegerArgument_DoughDto()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var expectedDough = new DoughDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var successResult = result.Result as CreatedResult;
            var resultDough = successResult.Value as DoughDto;

            var resultOfGettingNewDough = _fixture.DoughsController.Get(resultDough.Id);
            var successResultOfGettingNewDough = resultOfGettingNewDough.Result as OkObjectResult;
            var inBaseDough = successResultOfGettingNewDough.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough) && DoughEqualityChecker.IsDtoEqualsDto(expectedDough, inBaseDough));

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Get_IntIdentificatorIfNonExistingDough_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.DoughsController.Get("fdjvslkdfvb");
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
