namespace WebApi.Tests.AdditionalIngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerCreateTests
    {
        private readonly AdditionalIngredientControllerFixture _fixture;

        public AdditionalIngredientControllerCreateTests(AdditionalIngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDto_AdditionalIngredientDto()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var successResult = result.Result as CreatedResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            var resultOfGettingNewAdditionalIngredient = _fixture.AdditionalIngredientsController.Get(4);
            var successResultOfGettingNewAdditionalIngredient = resultOfGettingNewAdditionalIngredient.Result as OkObjectResult;
            var existingAdditionalIngredient = successResultOfGettingNewAdditionalIngredient.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient) && AdditionalIngredientEqualityChecker.IsDtoEqualsDto(expectedAdditionalIngredient, existingAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(4);
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientA.Name,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "There are more than twenty symbols",
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithNullPrice_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            string some = jsonResult.Value.ToString();

            // Assert
            Assert.True(string.Equals(expectedJsonResult.Value.ToString(), jsonResult.Value.ToString()));
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Test ingredient",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New ingredient",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            string first = expectedJsonResult.Value.ToString();
            string second = jsonResult.Value.ToString();

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_AdditionalIngredientCreateRequestDtoWithNullImage_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = null,
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
