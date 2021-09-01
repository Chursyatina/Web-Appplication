namespace WebAPI.MockFactory.Tests.Factory.Interfaces
{
    using System;
    using WebAPI.MockFactory.Tests.Utils;

    public interface ITestDataFactory : IDisposable
    {
        IControllerFactory CreateControllerFactory();

        IDatabaseInitializer CreateDatabaseInitializer();
    }
}
