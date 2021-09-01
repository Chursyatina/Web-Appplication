namespace WebApi.Tests.SharedData
{
    using Xunit;

    [CollectionDefinition("DoughTestsCollection")]
    public class DoughControllerFixtureCollection : ICollectionFixture<DoughControllerFixture>
    {
    }
}
