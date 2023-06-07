namespace WebApi.Tests.PizzaController
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response;
    using Domain.Models;
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
            var list = successResult.Value as IEnumerable<PizzaDto>;
            list = list.ToList();
            var listOfPizzas = list as List<PizzaDto>;
            listOfPizzas = SortPizzasByInBasePos(listOfPizzas);

            // Assert
            Assert.True(PizzaEqualityChecker.IsListOfDtosEqualsListOfModels(listOfPizzas, TestPizzas.AllPizzas));
        }

        public List<PizzaDto> SortPizzasByInBasePos(List<PizzaDto> list)
        {
            List<PizzaDto> sorted = new List<PizzaDto>();
            foreach (Pizza pizza in TestPizzas.AllPizzas)
            {
                PizzaDto pizz = list.Find(p => p.Name == pizza.Name);
                pizz.Ingredients = SortIngredientsByInBasePos(pizz.Ingredients.ToList(), pizza.Ingredients.ToList());
                sorted.Add(pizz);
            }

            return sorted;
        }

        public List<IngredientDto> SortIngredientsByInBasePos(List<IngredientDto> list, List<Ingredient> inBaseList)
        {
            List<IngredientDto> sorted = new List<IngredientDto>();
            foreach (Ingredient ingredient in inBaseList)
            {
                sorted.Add(list.Find(p => p.Name == ingredient.Name));
            }

            return sorted;
        }
    }
}
