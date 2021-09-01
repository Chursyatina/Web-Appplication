namespace WebAPI.MockFactory.Tests.Factory
{
    using Microsoft.Extensions.Logging;
    using WebApi.Controllers;
    using WebAPI.MockFactory.Tests.Factory.Interfaces;

    public class ControllerFactory : IControllerFactory
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ILoggerFactory _loggerFactory;

        public ControllerFactory(IServiceFactory serviceFactory, ILoggerFactory loggerFactory)
        {
            _serviceFactory = serviceFactory;
            _loggerFactory = loggerFactory;
        }

        public IngredientController CreateIngredientsController()
        {
            return new IngredientController(_loggerFactory.CreateLogger<IngredientController>(), _serviceFactory.CreateIngredientService());
        }

        public PizzaController CreatePizzasController()
        {
            return new PizzaController(_loggerFactory.CreateLogger<PizzaController>(), _serviceFactory.CreatePizzaService(), _serviceFactory.CreateIngredientService());
        }

        public SizeController CreateSizesController()
        {
            return new SizeController(_loggerFactory.CreateLogger<SizeController>(), _serviceFactory.CreateSizeService());
        }

        public DoughController CreateDoughsController()
        {
            return new DoughController(_loggerFactory.CreateLogger<DoughController>(), _serviceFactory.CreateDoughService());
        }

        public AdditionalIngredientController CreateAdditionalIngredientsController()
        {
            return new AdditionalIngredientController(_loggerFactory.CreateLogger<AdditionalIngredientController>(), _serviceFactory.CreateAdditionalIngredientService());
        }
    }
}
