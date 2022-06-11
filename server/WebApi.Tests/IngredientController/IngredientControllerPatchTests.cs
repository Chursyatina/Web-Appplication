namespace WebApi.Tests.IngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("IngredientTestsCollection")]
    public class IngredientControllerPatchTests
    {
        private readonly IngredientControllerFixture _fixture;

        public IngredientControllerPatchTests(IngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPatrtialUpdateRequestDto_IngredientDto()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = "New image",
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "New ingredient",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_NonExistingIdentificatorAndIngredientPartialUpdateRequestDto_NotFound()
        {
            // Arrange
            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = "NewName",
                Price = 100,
                ImageLink = "New image",
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.IngredientsController.Patch("Non existent", testIngredient);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithNullName_IngredientDto()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "Ingredient",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
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

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = TestIngredients.IngredientA.Name,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithNullPrice_IngredientDto()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = null,
                ImageLink = "New image",
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "New ingredient",
                Price = 101,
                ImageLink = "New image",
            };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0,1 and 1000.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPartialUpdateRequestDtoWithNullImage_IngredientDto()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = null,
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "New name",
                Price = 100,
                ImageLink = "Image",
            };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndIngredientPatrtialUpdateRequestDtoWithAllNullProperties_IngredientDto()
        {
            // Arrange
            var newIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "Image",
                Price = 101,
            };

            var testIngredient = new IngredientPatchRequestDto()
            {
                Name = null,
                Price = null,
                ImageLink = null,
            };

            var expectedIngredient = new IngredientDto()
            {
                Name = "Ingredient",
                Price = 101,
                ImageLink = "Image",
            };

            // Act
            var insertResult = _fixture.IngredientsController.Insert(newIngredient);
            var successedResult = insertResult.Result as CreatedResult;
            var resultOfInsertingIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Patch(resultOfInsertingIngredient.Id, testIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(resultIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(resultOfInsertingIngredient.Id);
        }
    }
}
