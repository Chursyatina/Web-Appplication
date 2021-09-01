namespace WebApi.Tests.IngredientController
{
    using System.Collections.Generic;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("IngredientTestsCollection")]
    public class IngredientControllerGetAllTests
    {
        private readonly IngredientControllerFixture _fixture;

        public IngredientControllerGetAllTests(IngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetAll_NoArgument_IngredientDtoList()
        {
            // Act
            var result = _fixture.IngredientsController.GetAll();
            var successResult = result.Result as OkObjectResult;
            var listOfIngredients = successResult.Value as List<IngredientDto>;

            // Assert
            Assert.True(IngredientEqualityChecker.IsListOfDtosEqualsListOfModels(listOfIngredients, TestIngredients.AllIngredients));
        }
    }
}
