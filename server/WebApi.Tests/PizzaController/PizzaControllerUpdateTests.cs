namespace WebApi.Tests.PizzaController
{
    using System.Collections.Generic;
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
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id, },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>() { resultIngredient, },
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_NonExistingIdentificatorAndPizzaUpdateRequestDto_NotFound()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient1",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id, },
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.PizzasController.Update("Non existent", testPizza);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());

            // Clear changes
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithEmptyIngredients_PizzaDtoWithEmptyIngredients()
        {
            // Arrange
            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza1",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient2",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza2",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = TestPizzas.PizzaA.Name,
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleItemImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient3",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza3",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient4",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza4",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "There are more then twenty symbols",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNullDescriptioin_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient5",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza5",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The Description field is required.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithDescriptioinLengthMoreThan150_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient6",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza6",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "At this string there are more than 150 symbols. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithDescriptioinLengthLessThen10_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient7",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza7",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Less 10",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNullImageLink_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient8",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza8",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithNonExistingIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient9",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza9",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleItemImageLink",
                Ingredients = new List<string>() { "Non existent", },
            };

            JsonResult expectedJsonResult = new JsonResult("There is no ingredient with id: Non existent") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithRepetitionOfIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient10",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza10",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleItemImageLink",
                Ingredients = new List<string>() { resultIngredient.Id, resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The identifier: " + resultIngredient.Id + " is repeated more than 1 time") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Update_IdentificatorAndPizzaUpdateRequestDtoWithEmptyIngredientsProperty_PizzaDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "Ingredient11",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza11",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaUpdateRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Update(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }
    }
}
