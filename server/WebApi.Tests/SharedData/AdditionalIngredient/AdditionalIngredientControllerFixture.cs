namespace WebApi.Tests.SharedData
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;
    using Infrastructure.EF;
    using Microsoft.Extensions.Logging;
    using WebApi.Controllers;
    using WebAPI.MockFactory.Tests.Data;
    using WebAPI.MockFactory.Tests.Factory;
    using WebAPI.MockFactory.Tests.Factory.Interfaces;
    using WebAPI.MockFactory.Tests.Utils;

    public class AdditionalIngredientControllerFixture : IDisposable
    {
        public AdditionalIngredientControllerFixture()
        {
            TestDataFactory = new TestDataFactory();

            DatabaseInitializer = TestDataFactory.CreateDatabaseInitializer();

            ControllerFactory = TestDataFactory.CreateControllerFactory();

            AdditionalIngredientsController = ControllerFactory.CreateAdditionalIngredientsController();

            InitializeDatabase(TestAdditionalIngredients.AllAdditionalIngredients);
        }

        public AdditionalIngredientController AdditionalIngredientsController { get; private set; }

        public IDatabaseInitializer DatabaseInitializer { get; private set; }

        public ITestDataFactory TestDataFactory { get; private set; }

        public IControllerFactory ControllerFactory { get; private set; }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            TestDataFactory?.Dispose();
        }

        public void InitializeDatabase(IEnumerable<AdditionalIngredient> initializingData)
        {
            DatabaseInitializer.InitializeDatabase((ILogger<DatabaseInitializer> logger, DatabaseContext databaseContext) =>
            {
                if (initializingData != null)
                {
                    databaseContext.AddRange(initializingData);
                }

                databaseContext.SaveChanges();
            });
        }
    }
}
