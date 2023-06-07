namespace WebApi.Tests.AdditionalIngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerUpdateTests
    {
        private readonly AdditionalIngredientControllerFixture _fixture;

        public AdditionalIngredientControllerUpdateTests(AdditionalIngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDto_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "New image",
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_NonExistingIdentificatorAndAdditionalIngredientUpdateRequestDto_NotFound()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "New image",
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.AdditionalIngredientsController.Update("Non existent", testAdditionalIngredient);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "There are more then twenty symbols",
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New one",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientA.Name,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithNullPrice_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New one",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "New name",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndAdditionalIngredientUpdateRequestDtoWithNullImage_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = null,
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Update(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }
    }
}
