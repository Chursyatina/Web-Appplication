using Application.Interfaces;
using Application.Services;
using WebAPI.MockFactory.Tests.Factory.Interfaces;

namespace WebAPI.MockFactory.Tests.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ServiceFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public IIngredientService CreateIngredientService()
        {
            return new IngredientService(_repositoryFactory.CreateIngredientRepository());
        }

        public IPizzaService CreatePizzaService()
        {
            return new PizzaService(_repositoryFactory.CreatePizzaRepository());
        }

        public ISizeService CreateSizeService()
        {
            return new SizeService(_repositoryFactory.CreateSizeRepository());
        }

        public IDoughService CreateDoughService()
        {
            return new DoughService(_repositoryFactory.CreateDoughRepository());
        }

        public IAdditionalIngredientService CreateAdditionalIngredientService()
        {
            return new AdditionalIngredientService(_repositoryFactory.CreateAdditionalIngredientRepository());
        }
    }
}
