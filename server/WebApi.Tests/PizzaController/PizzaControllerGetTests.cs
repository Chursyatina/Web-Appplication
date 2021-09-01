namespace WebApi.Tests.PizzaController
{
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("PizzaTestsCollection")]
    public class PizzaControllerGetTests
    {
        private readonly PizzaControllerFixture _fixture;

        public PizzaControllerGetTests(PizzaControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Get_IdentificatorIntegerArgument_PizzaDto()
        {
            // Act
            var result = _fixture.PizzasController.Get(1);
            var successResult = result.Result as OkObjectResult;
            var receivedPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsModel(receivedPizza, TestPizzas.PizzaA));
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingPizza_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.PizzasController.Get(20);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
