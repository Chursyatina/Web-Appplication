namespace WebApi.Tests.IngredientController
{
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
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
            // Act
            var result = _fixture.IngredientsController.Get(1);
            var successResult = result.Result as OkObjectResult;
            var receivedIngredient = successResult.Value as IngredientDto;

            // Assert
            Assert.True(IngredientEqualityChecker.IsDtoEqualsModel(receivedIngredient, TestIngredients.IngredientA));
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingIngredient_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.IngredientsController.Get(20);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
