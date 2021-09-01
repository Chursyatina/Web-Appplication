namespace WebApi.Tests.DoughController
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.MockFactory.Tests.Data;
    using WebApi.Tests.SharedData;
    using Xunit;

    [Collection("DoughTestsCollection")]
    public class DoughControllerGetAllTests
    {
        private readonly DoughControllerFixture _fixture;

        public DoughControllerGetAllTests(DoughControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetAll_NoArgument_DoughDtoList()
        {
            // Act
            var result = _fixture.DoughsController.GetAll();
            var successResult = result.Result as OkObjectResult;
            var listOfDoughs = successResult.Value as IEnumerable<DoughDto>;

            // Assert
            Assert.True(DoughEqualityChecker.IsListOfDtosEqualsListOfModels(listOfDoughs.ToList(), TestDoughs.AllDoughs));
        }
    }
}
