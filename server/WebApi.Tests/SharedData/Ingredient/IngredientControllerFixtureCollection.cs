namespace WebApi.Tests.SharedData
{
    using Xunit;

    [CollectionDefinition("IngredientTestsCollection")]
    public class IngredientControllerFixtureCollection : ICollectionFixture<IngredientControllerFixture>
    {
    }
}
