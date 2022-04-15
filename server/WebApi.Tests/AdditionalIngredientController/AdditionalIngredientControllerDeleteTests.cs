namespace WebApi.Tests.AdditionalIngredientController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerDeleteTests
    {
        private readonly AdditionalIngredientControllerFixture _fixture;

        public AdditionalIngredientControllerDeleteTests(AdditionalIngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgument_NoContent()
        {
            // Arrange
            var testAdditionalIngredient = new AdditionalIngredientCreateRequestDto()
            {
                Name = "New ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            NoContentResult expected = new NoContentResult();

            var resultOfCreating = _fixture.AdditionalIngredientsController.Insert(testAdditionalIngredient);
            var successResult = resultOfCreating.Result as CreatedResult;
            var resultOfCreatingAdditionalIngredient = successResult.Value as AdditionalIngredientDto;

            // Act
            var result = _fixture.AdditionalIngredientsController.Delete(resultOfCreatingAdditionalIngredient.Id);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.Equal(expected.ToString(), noContentResult.ToString());
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgumentOfNonExistingAdditionalIngredient_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.AdditionalIngredientsController.Delete("12");
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
