namespace WebApi.Tests.SharedData
{
    using Xunit;

    [CollectionDefinition("PizzaTestsCollection")]
    public class PizzaControllerFixtureCollection : ICollectionFixture<PizzaControllerFixture>
    {
    }
}
