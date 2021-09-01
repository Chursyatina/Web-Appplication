namespace WebApi.Tests.SizeController
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("SizeTestsCollection")]
    public class SizeControllerGetAllTests
    {
        private readonly SizeControllerFixture _fixture;

        public SizeControllerGetAllTests(SizeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetAll_NoArgument_SizeDtoList()
        {
            // Act
            var result = _fixture.SizesController.GetAll();
            var successResult = result.Result as OkObjectResult;
            var listOfSizes = successResult.Value as IEnumerable<SizeDto>;

            // Assert
            Assert.True(SizeEqualityChecker.IsListOfDtosEqualsListOfModels(listOfSizes.ToList(), TestSizes.AllSizes));
        }
    }
}
