namespace WebApi.Tests.AdditionalIngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerGetTests
    {
        private readonly AdditionalIngredientControllerFixture _fixture;

        public AdditionalIngredientControllerGetTests(AdditionalIngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Get_IdentificatorIntegerArgument_AdditionalIngredientDto()
        {
            // Arrange
            var newAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            var expectedIngredient = new AdditionalIngredientDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            // Act
            var addingResult = _fixture.AdditionalIngredientsController.Insert(newAdditionalIngredient);
            var successedResult = addingResult.Result as CreatedResult;
            var insertedIngredient = successedResult.Value as AdditionalIngredientDto;

            var result = _fixture.AdditionalIngredientsController.Get(insertedIngredient.Id);
            var successResult = result.Result as OkObjectResult;
            var receivedAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsDto(receivedAdditionalIngredient, expectedIngredient));

            // Clear changes
            _fixture.AdditionalIngredientsController.Delete(insertedIngredient.Id);
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingAdditionalIngredient_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.AdditionalIngredientsController.Get("Non existent");
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
