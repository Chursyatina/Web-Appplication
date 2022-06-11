namespace WebApi.Tests.AdditionalIngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerPatchTests
    {
        private readonly AdditionalIngredientControllerFixture _fixture;

        public AdditionalIngredientControllerPatchTests(AdditionalIngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPatrtialUpdateRequestDto_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = "New image",
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_NonExistingIdentificatorAndAdditionalIngredientPartialUpdateRequestDto_NotFound()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "NewName",
                Price = 100,
                ImageLink = "New image",
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch("Non existent", testAdditionalIngredient);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNullName_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "Ingredient",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
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

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
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

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNullPrice_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = null,
                ImageLink = "New image",
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "New ingredient",
                Price = 101,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNullImage_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = null,
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "Image",
            };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPatrtialUpdateRequestDtoWithAllNullProperties_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = null,
                Price = null,
                ImageLink = null,
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "Ingredient",
                Price = 101,
                ImageLink = "Image",
            };

            // Act
            var insertResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingAdditionalIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Patch(resultOfInsertingAdditionalIngredient.Id, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(resultOfInsertingAdditionalIngredient.Id);
        }
    }
}
