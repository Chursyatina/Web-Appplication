namespace WebApi.Tests.SizeController
{
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("SizeTestsCollection")]
    public class SizeControllerPatchTests
    {
        private readonly SizeControllerFixture _fixture;

        public SizeControllerPatchTests(SizeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Patch_IdentificatorAndSizePatrtialUpdateRequestDto_SizeDto()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            var expectedSize = new SizeDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var successResult = result.Result as OkObjectResult;
            var resultSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(resultSize, expectedSize));

            // Clear changes
            var initializedSize = new SizeUpdateRequestDto()
            {
                Name = TestSizes.SizeC.Name,
                PriceMultiplier = TestSizes.SizeC.PriceMultiplier,
            };

            _fixture.SizesController.Update(3, initializedSize);
        }

        [Fact]
        public void Patch_NonExistingIdentificatorAndSizePartialUpdateRequestDto_NotFound()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 6,
            };

            NotFoundResult expected = new NotFoundResult();

            // Act
            var result = _fixture.SizesController.Patch(20, testSize);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(expected.ToString(), notFoundResult.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndSizePartialUpdateRequestDtoWithNullName_SizeDto()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = null,
                PriceMultiplier = 6,
            };

            var expectedSize = new SizeDto()
            {
                Name = "Large",
                PriceMultiplier = 6,
            };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var successResult = result.Result as OkObjectResult;
            var resultSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(resultSize, expectedSize));

            // Clear changes
            var initializedSize = new SizeUpdateRequestDto()
            {
                Name = TestSizes.SizeC.Name,
                PriceMultiplier = TestSizes.SizeC.PriceMultiplier,
            };

            _fixture.SizesController.Update(3, initializedSize);
        }

        [Fact]
        public void Patch_IdentificatorAndSizePartialUpdateRequestDtoWithNameLengthMoreThan20_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = "There are more then twenty symbols",
                PriceMultiplier = 6,
            };

            JsonResult expectedJsonResult = new JsonResult("The field Name must be a string with a minimum length of 1 and a maximum length of 20.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndSizePartialUpdateRequestDtoWithNullPriceMultiplier_SizeDto()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = null,
            };

            var expectedSize = new SizeDto()
            {
                Name = "NewName",
                PriceMultiplier = 2,
            };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var successResult = result.Result as OkObjectResult;
            var resultSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(resultSize, expectedSize));

            // Clear changes
            var initializedSize = new SizeUpdateRequestDto()
            {
                Name = TestSizes.SizeC.Name,
                PriceMultiplier = TestSizes.SizeC.PriceMultiplier,
            };

            _fixture.SizesController.Update(3, initializedSize);
        }

        [Fact]
        public void Patch_IdentificatorAndSizePartialUpdateRequestDtoWithPriceMultiplierMoreThan7_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 8,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndSizePartialUpdateRequestDtoWithPriceMultiplierLessThanOneTenth_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = "NewName",
                PriceMultiplier = 0.01m,
            };

            JsonResult expectedJsonResult = new JsonResult("The field PriceMultiplier must be between 0.1 and 7.") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndSizePartialUpdateRequestDtoWithNotUniqueName_Error400WithCorrectResponseBody()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = TestSizes.SizeA.Name,
                PriceMultiplier = 7,
            };

            JsonResult expectedJsonResult = new JsonResult("Enity with such name already exists") { StatusCode = 400, };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var badRequestResult = result.Result as BadRequestObjectResult;
            var jsonResult = badRequestResult.Value as JsonResult;

            // Assert
            Assert.True(expectedJsonResult.Value.ToString() == jsonResult.Value.ToString());
        }

        [Fact]
        public void Patch_IdentificatorAndSizePatrtialUpdateRequestDtoWithAllNullProperties_SizeDto()
        {
            // Arrange
            var testSize = new SizePatchRequestDto()
            {
                Name = null,
                PriceMultiplier = null,
            };

            var expectedSize = new SizeDto()
            {
                Name = "Large",
                PriceMultiplier = 2,
            };

            // Act
            var result = _fixture.SizesController.Patch(3, testSize);
            var successResult = result.Result as OkObjectResult;
            var resultSize = successResult.Value as SizeDto;

            // Assert
            Assert.True(SizeEqualityChecker.IsDtoEqualsDto(resultSize, expectedSize));
        }
    }
}
