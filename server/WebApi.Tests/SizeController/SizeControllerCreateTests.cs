namespace WebApi.Tests.SizeController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("SizeTestsCollection")]
    public class SizeControllerCreateTests
    {
        private readonly SizeControllerFixture _fixture;

        public SizeControllerCreateTests(SizeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_SizeCreateRequestDto_SizeDto()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "TestSize",
                PriceMultiplier = 7,
            };

            var expectedSize = new SizeDto()
            {
                Name = "TestSize",
                PriceMultiplier = 7,
            };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var successResult = result.Result as CreatedResult;
            var resultSize = successResult.Value as SizeDto;

            var resultOfGettingNewSize = _fixture.SizesController.Get(resultSize.Id);
            var successResultOfGettingNewSize = resultOfGettingNewSize.Result as OkObjectResult;
            var inBaseSize = successResultOfGettingNewSize.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(resultSize, expectedSize) && SizeEqualityChecker.IsDtoEqualsDto(expectedSize, inBaseSize));

            // Clear changes
            _fixture.SizesController.Delete(resultSize.Id);
        }

        [Fact]
        public void Create_SizeCreateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = TestSizes.SizeA.Name,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_SizeCreateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_SizeCreateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "There are more than twenty symbols",
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_SizeCreateRequestDtoWithNullPriceMultiplier_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "TestSize",
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_SizeCreateRequestDtoWithPriceMultiplierMoreThan7_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "TestSize",
                PriceMultiplier = 8,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_SizeCreateRequestDtoWithPriceMultiplierLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizeCreateRequestDto()
            {
                Name = "TestSize",
                PriceMultiplier = 0.01m,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Insert(testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
