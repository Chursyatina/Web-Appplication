namespace WebApi.Tests.DoughController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("DoughTestsCollection")]
    public class DoughControllerPatchTests
    {
        private readonly DoughControllerFixture _fixture;

        public DoughControllerPatchTests(DoughControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPatrtialUpdateRequestDto_DoughDto()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            var expectedDough = new DoughDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var successResult = result.Result as OkObjectResult;
            var resultDough = successResult.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough));

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_NonExistingIdentificatorAndDoughPartialUpdateRequestDto_NotFound()
        {
            // Arrange
            var testDough = new DoughPatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.DoughsController.Patch("non existent", testDough);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPartialUpdateRequestDtoWithNullName_DoughDto()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = null,
                PriceMultiplier = 6,
            };

            var expectedDough = new DoughDto()
            {
                Name = "New dough",
                PriceMultiplier = 6,
            };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var successResult = result.Result as OkObjectResult;
            var resultDough = successResult.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough));

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPartialUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = "There are more then twenty symbols",
                PriceMultiplier = 6,
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPartialUpdateRequestDtoWithNullPriceMultiplier_DoughDto()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 2,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = null,
            };

            var expectedDough = new DoughDto()
            {
                Name = "NewName",
                PriceMultiplier = 2,
            };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var successResult = result.Result as OkObjectResult;
            var resultDough = successResult.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough));

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPartialUpdateRequestDtoWithPriceMultiplierMoreThan7_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 8,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPartialUpdateRequestDtoWithPriceMultiplierLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 0.01m,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0,1 and 7.") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPartialUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 7,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = TestDoughs.DoughA.Name,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }

        [Fact]
        public void Patch_IdentificatorAndDoughPatrtialUpdateRequestDtoWithAllNullProperties_DoughDto()
        {
            // Arrange
            var newDough = new DoughCreateRequestDto()
            {
                Name = "New dough",
                PriceMultiplier = 2,
            };

            var testDough = new DoughPatchRequestDto()
            {
                Name = null,
                PriceMultiplier = null,
            };

            var expectedDough = new DoughDto()
            {
                Name = "New dough",
                PriceMultiplier = 2,
            };

            // Act
            var insertResult = _fixture.DoughsController.Insert(newDough);
            var successedResult = insertResult.Result as CreatedResult;
            var inBaseDough = successedResult.Value as DoughDto;

            var result = _fixture.DoughsController.Patch(inBaseDough.Id, testDough);
            var successResult = result.Result as OkObjectResult;
            var resultDough = successResult.Value as DoughDto;

            // Assert
            Assert.True(DoughEqualityChecker.IsDtoEqualsDto(resultDough, expectedDough));

            // Clear changes
            _fixture.DoughsController.Delete(inBaseDough.Id);
        }
    }
}
