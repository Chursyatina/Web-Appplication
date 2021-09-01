namespace WebAPI.MockFactory.Tests.Factory.Interfaces
{
    using Application.Interfaces;

    public interface IServiceFactory
    {
        IPizzaService CreatePizzaService();

        IIngredientService CreateIngredientService();

        ISizeService CreateSizeService();

        IDoughService CreateDoughService();

        IAdditionalIngredientService CreateAdditionalIngredientService();
    }
}
