namespace WebAPI.MockFactory.Tests.Factory
{
    using Domain.Repository;
    using Infrastructure.Repository;
    using WebAPI.MockFactory.Tests.Factory.Interfaces;

    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IRepositoryContextFactory _repositoryContextFactory;

        public RepositoryFactory(IRepositoryContextFactory repositoryContextFactory)
        {
            _repositoryContextFactory = repositoryContextFactory;
        }

        public IPizzaRepository CreatePizzaRepository()
        {
            return new PizzaRepository(_repositoryContextFactory.CreateDatabaseContext());
        }

        public IIngredientRepository CreateIngredientRepository()
        {
            return new IngredientRepository(_repositoryContextFactory.CreateDatabaseContext());
        }

        public ISizeRepository CreateSizeRepository()
        {
            return new SizeRepository(_repositoryContextFactory.CreateDatabaseContext());
        }

        public IDoughRepository CreateDoughRepository()
        {
            return new DoughRepository(_repositoryContextFactory.CreateDatabaseContext());
        }

        public IAdditionalIngredientRepository CreateAdditionalIngredientRepository()
        {
            return new AdditionalIngredientRepository(_repositoryContextFactory.CreateDatabaseContext());
        }
    }
}
