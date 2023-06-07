namespace WebApi.Tests.IngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("IngredientTestsCollection")]
    public class IngredientControllerGetTests
    {
        private readonly IngredientControllerFixture _fixture;

        public IngredientControllerGetTests(IngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Get_IdentificatorIntegerArgument_IngredientDto()
        {
            var newAdditionalIngredient = new IngredientCreateRequestDto()
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
            var addingResult = _fixture.IngredientsController.Insert(newAdditionalIngredient);
            var successedResult = addingResult.Result as CreatedResult;
            var insertedIngredient = successedResult.Value as IngredientDto;

            var result = _fixture.IngredientsController.Get(insertedIngredient.Id);
            var successResult = result.Result as OkObjectResult;
            var receivedIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsDto(receivedIngredient, expectedIngredient));

            // Clear changes
            _fixture.IngredientsController.Delete(insertedIngredient.Id);
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingIngredient_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.IngredientsController.Get("Non exisntent");
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
