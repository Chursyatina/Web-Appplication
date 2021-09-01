namespace WebApi.Tests.PizzaController
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("PizzaTestsCollection")]
    public class PizzaControllerGetAllTests
    {
        private readonly PizzaControllerFixture _fixture;

        public PizzaControllerGetAllTests(PizzaControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetAll_NoArguments_PizzaDtoList()
        {
            // Act
            var result = _fixture.PizzasController.GetAll();
            var successResult = result.Result as OkObjectResult;
            var listOfPizzas = successResult.Value as IEnumerable<PizzaDto>;

            // Assert
            Assert.True(PizzaEqualityChecker.IsListOfDtosEqualsListOfModels(listOfPizzas.ToList(), TestPizzas.AllPizzas));
        }
    }
}
