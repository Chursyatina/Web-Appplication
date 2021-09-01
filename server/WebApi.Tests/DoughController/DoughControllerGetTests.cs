namespace WebApi.Tests.DoughController
{
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
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
            // Act
            var result = _fixture.DoughsController.Get(1);
            var successResult = result.Result as OkObjectResult;
            var receivedDough = successResult.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsModel(receivedDough, TestDoughs.DoughA));
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingDough_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.DoughsController.Get(20);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
