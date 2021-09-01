namespace WebApi.Tests.PizzaController
{
    using System.Collections.Generic;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("PizzaTestsCollection")]
    public class PizzaControllerUpdateTests
    {
        private readonly PizzaControllerFixture _fixture;

        public PizzaControllerUpdateTests(PizzaControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDto_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaUpdateRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Update(3, initializedPizza);
        }

        [Fact]
        public void Update_NonExistingIdentificatorAndPizzaUpdateRequestDto_NotFound()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.PizzasController.Update(20, testPizza);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithEmptyIngredients_PizzaDtoWithEmptyIngredients()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<int>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaUpdateRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Update(3, initializedPizza);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = TestPizzas.PizzaA.Name,
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleItemImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "There are more then twenty symbols",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNullDescriptioin_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The Description field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithDescriptioinLengthMoreThan150_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "At this string there are more than 150 symbols. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithDescriptioinLengthLessThen10_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Less 10",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNullImageLink_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNonExistingIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleItemImageLink",
                Ingredients = new List<int>() { 120, },
            };

            JsonResult expectedJsonResult = new JsonResult("There is no ingredient with id: 120") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithRepetitionOfIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleItemImageLink",
                Ingredients = new List<int>() { 1, 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The identifier: 1 is repeated more than 1 time") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithEmptyIngredientsProperty_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<int>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var result = _fixture.PizzasController.Update(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaUpdateRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Update(3, initializedPizza);
        }
    }
}
