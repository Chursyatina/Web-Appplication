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
            var result = _fixture.AdditionalIngredientsController.Patch(2, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            var initializedIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientB.Name,
                Price = TestAdditionalIngredients.AdditionalIngredientB.Price,
                ImageLink = TestAdditionalIngredients.AdditionalIngredientB.ImageLink,
            };

            _fixture.AdditionalIngredientsController.Update(2, initializedIngredient);
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
            var result = _fixture.AdditionalIngredientsController.Patch(20, testAdditionalIngredient);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNullName_AdditionalIngredientDto()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = null,
                Price = 100,
                ImageLink = "New image",
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "Mozzarella",
                Price = 100,
                ImageLink = "New image",
            };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(1, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            var initializedIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientA.Name,
                Price = TestAdditionalIngredients.AdditionalIngredientA.Price,
                ImageLink = TestAdditionalIngredients.AdditionalIngredientA.ImageLink,
            };

            _fixture.AdditionalIngredientsController.Update(1, initializedIngredient);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "There are more then twenty symbols",
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(2, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientA.Name,
                Price = 100,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(2, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNullPrice_AdditionalIngredientDto()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = null,
                ImageLink = "New image",
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "New ingredient",
                Price = 59,
                ImageLink = "New image",
            };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(1, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            var initializedIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientA.Name,
                Price = TestAdditionalIngredients.AdditionalIngredientA.Price,
                ImageLink = TestAdditionalIngredients.AdditionalIngredientA.ImageLink,
            };

            _fixture.AdditionalIngredientsController.Update(1, initializedIngredient);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithPriceMoreThan1000_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 1001,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0.1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(2, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithPriceLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = "New ingredient",
                Price = 0.01m,
                ImageLink = "New image",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Price must be between 0.1 and 1000.") { StatusCode = 400, };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(2, testAdditionalIngredient);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPartialUpdateRequestDtoWithNullImage_AdditionalIngredientDto()
        {
            // Arrange
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
                ImageLink = "IngredientA image",
            };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(1, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));

            // Clear changes
            var initializedIngredient = new AdditionalIngredientUpdateRequestDto()
            {
                Name = TestAdditionalIngredients.AdditionalIngredientA.Name,
                Price = TestAdditionalIngredients.AdditionalIngredientA.Price,
                ImageLink = TestAdditionalIngredients.AdditionalIngredientA.ImageLink,
            };

            _fixture.AdditionalIngredientsController.Update(1, initializedIngredient);
        }

        [Fact]
        public void Patch_IdentificatorAndAdditionalIngredientPatrtialUpdateRequestDtoWithAllNullProperties_AdditionalIngredientDto()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientPatchRequestDto()
            {
                Name = null,
                Price = null,
                ImageLink = null,
            };

            var expectedAdditionalIngredient = new AdditionalIngredientDto()
            {
                Name = "Mozzarella",
                Price = 59,
                ImageLink = "IngredientA image",
            };

            // Act
            var result = _fixture.AdditionalIngredientsController.Patch(1, testAdditionalIngredient);
            var successResult = result.Result as OkObjectResult;
            var resultAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(resultAdditionalIngredient, expectedAdditionalIngredient));
        }
    }
}
