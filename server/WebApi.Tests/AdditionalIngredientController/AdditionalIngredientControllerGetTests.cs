namespace WebApi.Tests.AdditionalIngredientController
{
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
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
            // Act
            var result = _fixture.AdditionalIngredientsController.Get(1);
            var successResult = result.Result as OkObjectResult;
            var receivedAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsDtoEqualsModel(receivedAdditionalIngredient, TestAdditionalIngredients.AdditionalIngredientA));
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingAdditionalIngredient_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.AdditionalIngredientsController.Get(20);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
