namespace WebApi.Tests.IngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("IngredientTestsCollection")]
    public class IngredientControllerUpdateTests
    {
        private readonly IngredientControllerFixture _fixture;

        public IngredientControllerUpdateTests(IngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDto_IngredientDto()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "New image",
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_NonExistingIdentificatorAndIngredientUpdateRequestDto_NotFound()
        {
            // Arrange
            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "New image",
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.IngredientsController.Update("Non existent", testIngredient);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "There are more then twenty symbols",
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = TestIngredients.IngredientA.Name,
                Price = 1000,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithNullPrice_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndIngredientUpdateRequestDtoWithNullImage_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientUpdateRequestDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = null,
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Update(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }
    }
}
