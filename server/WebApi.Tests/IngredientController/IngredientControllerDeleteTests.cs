namespace WebApi.Tests.IngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("IngredientTestsCollection")]
    public class IngredientControllerDeleteTests
    {
        private readonly IngredientControllerFixture _fixture;

        public IngredientControllerDeleteTests(IngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgument_NoContent()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            NoContentResult expected = new NoContentResult();

            var resultOfCreating = _fixture.IngredientsController.Insert(testIngredient);
            var successResult = resultOfCreating.Result as CreatedResult;
            var resultOfCreatingIngredient = successResult.Value as IngredientDto;

            // Act
            var result = _fixture.IngredientsController.Delete(resultOfCreatingIngredient.Id);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.Equal(expected.ToString(), noContentResult.ToString());
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgumentOfNonExistingIngredient_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.IngredientsController.Delete("Non existent");
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
