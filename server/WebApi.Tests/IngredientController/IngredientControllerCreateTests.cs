namespace WebApi.Tests.IngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("IngredientTestsCollection")]
    public class IngredientControllerCreateTests
    {
        private readonly IngredientControllerFixture _fixture;

        public IngredientControllerCreateTests(IngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_IngredientCreateRequestDto_IngredientDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var successResult = result.Result as CreatedResult;
            var resultIngredient = successResult.Value as IngredientDto;

            var resultOfGettingNewIngredient = _fixture.IngredientsController.Get(4);
            var successResultOfGettingNewIngredient = resultOfGettingNewIngredient.Result as OkObjectResult;
            var existingIngredient = successResultOfGettingNewIngredient.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient) && IngredientEqualityChecker.IsDtoEqualsDto(expectedIngredient, existingIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(4);
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = TestIngredients.IngredientA.Name,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "There are more than twenty symbols",
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithNullPrice_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Test ingredient",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(string.Equals(expectedJsonResult.Value.ToString(), jsonResult.Value.ToString()));
        }

        [Fact]
        public void Create_IngredientCreateRequestDtoWithNullImage_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New Ingredient",
                Price = 100,
                ImageLink = null,
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.IngredientsController.Insert(testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
