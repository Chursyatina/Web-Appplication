namespace WebApi.Tests.PizzaController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("PizzaTestsCollection")]
    public class PizzaControllerDeleteTests
    {
        private readonly PizzaControllerFixture _fixture;

        public PizzaControllerDeleteTests(PizzaControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgument_NoContent()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImage",
                SingleItemImageLink = "SomeSingleItemTestImage",
            };

            NoContentResult expected = new NoContentResult();

            var resultOfCreating = _fixture.PizzasController.Insert(testPizza);
            var successResult = resultOfCreating.Result as CreatedResult;
            var resultOfCreatingPizza = successResult.Value as PizzaDto;

            // Act
            var result = _fixture.PizzasController.Delete(resultOfCreatingPizza.Id);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.Equal(expected.ToString(), noContentResult.ToString());
        }

        [Fact]
        public void Delete_IdentificatorIntegerArgumentOfNonExistingPizza_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.PizzasController.Delete(12);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
