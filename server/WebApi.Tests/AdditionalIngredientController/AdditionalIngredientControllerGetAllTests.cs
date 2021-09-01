namespace WebApi.Tests.AdditionalIngredientController
{
    using System.Collections.Generic;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerGetAllTests
    {
        private readonly AdditionalIngredientControllerFixture _fixture;

        public AdditionalIngredientControllerGetAllTests(AdditionalIngredientControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetAll_NoArgument_AdditionalIngredientDtoList()
        {
            // Act
            var result = _fixture.AdditionalIngredientsController.GetAll();
            var successResult = result.Result as OkObjectResult;
            var listOfAdditionalIngredients = successResult.Value as List<AdditionalIngredientDto>;

            // Assert
            Assert.True(AdditionalIngredientEqualityChecker.IsListOfDtosEqualsListOfModels(listOfAdditionalIngredients, TestAdditionalIngredients.AllAdditionalIngredients));
        }
    }
}
