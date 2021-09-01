namespace WebApi.Tests.PizzaController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("PizzaTestsCollection")]
    public class PizzaControllerCreateTests
    {
        private readonly PizzaControllerFixture _fixture;

        public PizzaControllerCreateTests(PizzaControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_PizzaCreateRequestDto_PizzaDto()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
            };

            var expectedPizza = new PizzaDto()
            {
                Name = "TestPizza",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "SomeSingleItemTestImageLink",
                Ingredients = new System.Collections.Generic.List<IngredientDto>(),
            };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var successResult = result.Result as CreatedResult;
            var resultPizza = successResult.Value as PizzaDto;

            var resultOfGettingNewPizza = _fixture.PizzasController.Get(4);
            var successResultOfGettingNewPizza = resultOfGettingNewPizza.Result as OkObjectResult;
            var inBasePizza = successResultOfGettingNewPizza.Value as PizzaDto;

            // Assert
            Assert.True(PizzaEqualityChecker.IsDtoEqualsDto(resultPizza, expectedPizza) && PizzaEqualityChecker.IsDtoEqualsDto(expectedPizza, inBasePizza));

            // Clear changes
            _fixture.PizzasController.Delete(4);
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = TestPizzas.PizzaA.Name,
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
                SingleItemImageLink = "TestPizzaSingleImageLink",
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithNullName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
            };

            JsonResult expectedJsonResult = new JsonResult("The Name field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "There are more than twenty symbols",
                Description = "TestPizzaDescription",
                ImageLink = "TestPizzaImageLink",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithNullDescription_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                ImageLink = "TestPizzaImageLink",
            };

            JsonResult expectedJsonResult = new JsonResult("The Description field is required.") { StatusCode = 400 };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithDescriptionLengthMoreThen150_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                Description = "At this string there are more than 150 symbols. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                ImageLink = "TestPizzaImageLink",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithDescriptionLengthLessThen10_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                Description = "Less 10",
                ImageLink = "TestPizzaImageLink",
            };

            JsonResult expectedJsonResult = new JsonResult("The field Description must be a string with a minimum length of 10 and a maximum length of 150.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Create_PizzaCreateRequestDtoWithNullImageLink_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testPizza = new PizzaCreateRequestDto()
            {
                Name = "TestPizza",
                Description = "TestPizzaDescription",
            };

            JsonResult expectedJsonResult = new JsonResult("The ImageLink field is required.") { StatusCode = 400, };

            // Act
            var result = _fixture.PizzasController.Insert(testPizza);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }
    }
}
