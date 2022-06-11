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
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { resultIngredient, },
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_NonExistingIdentificatorAndPizzaPartialUpdateRequestDto_NotFound()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient1",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza1",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch("Non existent", testPizza);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithEmptyIngredients_PizzaDtoWithEmptyIngredients()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient2",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<string>(),
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullName_PizzaDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient3",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = null,
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "TestPizza3",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { resultIngredient, },
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullDescription_PizzaDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient4",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = null,
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "TestPizzaDescription",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { resultIngredient, },
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullImageLink_PizzaDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient5",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = null,
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeTestSibgleImage",
                Ingredients = new List<IngredientDto>() { resultIngredient, },
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithNullIngredients_PizzaDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient6",
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
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleImageLink",
                Ingredients = null,
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                SingleItemImageLink = "SomeTestSingleImageLink",
                Ingredients = new List<IngredientDto>() { resultIngredient },
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var successResult = result.Result as OkObjectResult;
            var resultPizza = successResult.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza));

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient7",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = TestPizzas.PizzaA.Name,
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient8",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "There are more then twenty symbols",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>(),
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithDescriptionLengthMoreThan150_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient9",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "At this string there are more than 150 symbols. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>(),
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithDescriptioinLengthLessThen10_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient10",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "Meat",
                Description = "Less 10",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id },
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithNonExistingIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient11",
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

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>() { "Non exisntent", },
            };

            JsonResult expectedJsonResult = new JsonResult("There is no ingredient with id: Non exisntent") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaUpdateRequestDtoWithRepetitionOfIngredientIdentificator_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient12",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza12",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

            var testPizza = new PizzaPatchRequestDto()
            {
                Name = "New Pizza",
                Description = "Some meat description",
                ImageLink = "SomeTestImageLink",
                Ingredients = new List<string>() { resultIngredient.Id, resultIngredient.Id, },
            };

            JsonResult expectedJsonResult = new JsonResult("The identifier: " + resultIngredient.Id + " is repeated more than 1 time") { StatusCode = 400, };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.PizzasController.Delete(addedPizza.Id);
            _fixture.IngredientsController.Delete(resultIngredient.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndPizzaPartialUpdateRequestDtoWithAllNullProperties_PizzaDto()
        {
            // Arrange
            var testIngredient = new IngredientCreateRequestDto()
            {
                Name = "New ingredient13",
                ImageLink = "New image",
                Price = 101,
            };

            var addingIngredientResult = _fixture.IngredientsController.Insert(testIngredient);
            var successedingredientResult = addingIngredientResult.Result as CreatedResult;
            var resultIngredient = successedingredientResult.Value as IngredientDto;

            var forInsertPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza13",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<string>(),
            };

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
                Name = "TestPizza13",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new List<IngredientDto>(),
            };

            // Act
            var addingResult = _fixture.PizzasController.Insert(forInsertPizza);
            var successedResult = addingResult.Result as CreatedResult;
            var addedPizza = successedResult.Value as PizzaDto;

            var result = _fixture.PizzasController.Patch(addedPizza.Id, testPizza);
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
