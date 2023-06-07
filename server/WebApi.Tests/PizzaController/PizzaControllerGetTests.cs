namespace WebApi.Tests.PizzaController
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
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
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "New name",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "TestPizzaSingleImageLink",
                Ingredients = new List<string>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New name",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "TestPizzaSingleImageLink",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(testPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Get(addedPizza.Id);
            var successResult = result.Result as OkObjectResult;
            var receivedPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(receivedPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
        }

        [Fact]
        public void Get__IntIdentificatorIfNonExistingPizza_NotFound()
        {
            // Arrange
            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.PizzasController.Get("Non existent");
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }
    }
}
