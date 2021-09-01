namespace WebApi.Tests.SizeController
{
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
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
            // Act
            var result = _fixture.SizesController.Get(1);
            var successResult = result.Result as OkObjectResult;
            var receivedSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsModel(receivedSize, TestSizes.SizeA));
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingSize_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.SizesController.Get(20);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
