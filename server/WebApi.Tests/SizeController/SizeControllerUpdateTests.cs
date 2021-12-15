namespace WebApi.Tests.SizeController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("SizeTestsCollection")]
    public class SizeControllerUpdateTests
    {
        private readonly SizeControllerFixture _fixture;

        public SizeControllerUpdateTests(SizeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Update_IdentificatorAndSizeUpdateRequestDto_SizeDto()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = "New name",
                PriceMultiplier = 7,
            };

            var expectedSize = new SizeDto()
            {
                Name = "New name",
                PriceMultiplier = 7,
            };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var successResult = result.Result as OkObjectResult;
            var resultSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(resultSize, expectedSize));

            // Clear changes
            var initializedSize = new SizeUpdateRequestDto()
            {
                Name = TestSizes.SizeC.Name,
                PriceMultiplier = TestSizes.SizeC.PriceMultiplier,
            };

            _fixture.SizesController.Update(3, initializedSize);
        }

        [Fact]
        public void Update_NonExistingIdentificatorAndSizeUpdateRequestDto_NotFound()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.SizesController.Update(20, testSize);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndSizeUpdateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = null,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndSizeUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = "There are more then twenty symbols",
                PriceMultiplier = 6,
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IdentificatorAndSizeUpdateRequestDtoWithNullPriceMultiplier_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = "TestSize",
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndSizeUpdateRequestDtoWithPriceMultiplierMoreThan7_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 8,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndSizeUpdateRequestDtoWithPriceMultiplierLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 0.01m,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndSizeUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeUpdateRequestDto()
            {
                Name = TestSizes.SizeA.Name,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Update(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
