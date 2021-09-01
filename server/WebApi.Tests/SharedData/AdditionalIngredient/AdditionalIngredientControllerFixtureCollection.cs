namespace WebApi.Tests.SharedData
{
    using Xunit;

    [CollectionDefinition("AdditionalIngredientTestsCollection")]
    public class AdditionalIngredientControllerFixtureCollection : ICollectionFixture<AdditionalIngredientControllerFixture>
    {
    }
}
