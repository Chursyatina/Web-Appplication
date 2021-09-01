namespace WebAPI.MockFactory.Tests.Factory.Interfaces
{
    using Domain.Repository;

    public interface IRepositoryFactory
    {
        IPizzaRepository CreatePizzaRepository();

        IIngredientRepository CreateIngredientRepository();

        ISizeRepository CreateSizeRepository();

        IDoughRepository CreateDoughRepository();

        IAdditionalIngredientRepository CreateAdditionalIngredientRepository();
    }
}
