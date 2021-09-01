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
    public class PizzaControllerPatchTests
    {
        private PizzaControllerFixture _fixture;

        public PizzaControllerPatchTests(PizzaControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDto_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<int>() { 1, },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            var result2 = _fixture.PizzasController.Patch(3, initializedPizza);
        }

        [Fact]
        public void Patch_NonExistingIdentificatorAndPizzaPartialUpdateRequestDto_NotFound()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.PizzasController.Patch(20, testPizza);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithEmptyIngredients_PizzaDtoWithEmptyIngredients()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<int>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Patch(3, initializedPizza);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullName_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = null,
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<int>() { 1, },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Patch(3, initializedPizza);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullDescription_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = null,
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<int>() { 1, },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Patch(3, initializedPizza);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullImageLink_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = null,
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<int>() { 1, },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "Image link",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.SingleItemImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Patch(3, initializedPizza);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullIngredients_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleImageLink",
                Ingredients = null,
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleImageLink",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), TestPizzas.IngredientB.ToViewModel(), TestPizzas.IngredientC.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            var initializedPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaC.Name,
                Description = TestPizzas.PizzaC.Description,
                ImageLink = TestPizzas.PizzaC.ImageLink,
                SingleItemImageLink = TestPizzas.PizzaC.ImageLink,
                Ingredients = new List<int>() { 1, 2, 3 },
            };

            _fixture.PizzasController.Patch(3, initializedPizza);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaA.Name,
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "There are more then twenty symbols",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithDescriptionLengthMoreThan150_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "At this string there are more than 150 symbols. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithDescriptioinLengthLessThen10_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Less 10",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithNonExistingIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 120, },
            };

            JsonResult expectedJsonResult = new JsonResult("There is no ingredient with id: 120") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithRepetitionOfIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<int>() { 1, 1, },
            };

            JsonResult expectedJsonResult = new JsonResult("The identifier: 1 is repeated more than 1 time") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithAllNullProperties_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaPatchRequestDto()
            {
                Name = null,
                Description = null,
                ImageLink = null,
                SingleItemImageLink = null,
                Ingredients = null,
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "Meat",
                Description = "Some meat description",
                ImageLink = "Image link",
                SingleItemImageLink = "Single image link",
                Ingredients = new List<IngredientDto>() { TestPizzas.IngredientA.ToViewModel(), TestPizzas.IngredientB.ToViewModel(), TestPizzas.IngredientC.ToViewModel(), },
            };

            // Act
            var result = _fixture.PizzasController.Patch(3, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));
        }
    }
}
