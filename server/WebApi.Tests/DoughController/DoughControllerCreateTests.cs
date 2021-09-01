namespace WebApi.Tests.DoughController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("DoughTestsCollection")]
    public class DoughControllerCreateTests
    {
        private readonly DoughControllerFixture _fixture;

        public DoughControllerCreateTests(DoughControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_DoughCreateRequestDto_DoughDto()
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

            var resultOfGettingNewDough = _fixture.DoughsController.Get(3);
            var successResultOfGettingNewDough = resultOfGettingNewDough.Result as OkObjectResult;
            var inBaseDough = successResultOfGettingNewDough.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough) && DoughEqualityChecker.IsDtoEqualsDto(expectedDough, inBaseDough));

            // Clear changes
            _fixture.DoughsController.Delete(3);
        }

        [Fact]
        public void Create_DoughCreateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = TestDoughs.DoughA.Name,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_DoughCreateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_DoughCreateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = "There are more than twenty symbols",
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_DoughCreateRequestDtoWithNullPriceMultiplier_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = "TestDough",
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_DoughCreateRequestDtoWithPriceMultiplierMoreThan7_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = "TestDough",
                PriceMultiplier = 8,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_DoughCreateRequestDtoWithPriceMultiplierLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughCreateRequestDto()
            {
                Name = "TestDough",
                PriceMultiplier = 0.01m,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Insert(testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
