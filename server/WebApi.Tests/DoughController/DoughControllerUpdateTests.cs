namespace WebApi.Tests.DoughController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("DoughTestsCollection")]
    public class DoughControllerUpdateTests
    {
        private readonly DoughControllerFixture _fixture;

        public DoughControllerUpdateTests(DoughControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Update_IdentificatorAndDoughUpdateRequestDto_DoughDto()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = "New name",
                PriceMultiplier = 7,
            };

            var expectedDough = new DoughDto()
            {
                Name = "New name",
                PriceMultiplier = 7,
            };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var successResult = result.Result as OkObjectResult;
            var resultDough = successResult.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough));

            // Clear changes
            var initializedDough = new DoughUpdateRequestDto()
            {
                Name = TestDoughs.DoughB.Name,
                PriceMultiplier = TestDoughs.DoughB.PriceMultiplier,
            };

            _fixture.DoughsController.Update(2, initializedDough);
        }

        [Fact]
        public void Update_NonExistingIdentificatorAndDoughUpdateRequestDto_NotFound()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.DoughsController.Update(20, testDough);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndDoughUpdateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = null,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndDoughUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = "There are more then twenty symbols",
                PriceMultiplier = 6,
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IdentificatorAndDoughUpdateRequestDtoWithNullPriceMultiplier_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = "TestDough",
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndDoughUpdateRequestDtoWithPriceMultiplierMoreThan7_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 8,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndDoughUpdateRequestDtoWithPriceMultiplierLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 0.01m,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndDoughUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testDough = new DoughUpdateRequestDto()
            {
                Name = TestDoughs.DoughA.Name,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.DoughsController.Update(2, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
