namespace WebAPI.MockFactory.Tests.Factory.Interfaces
{
    using WebApi.Controllers;

    public interface IControllerFactory
    {
        PizzaController CreatePizzasController();

        IngredientController CreateIngredientsController();

        SizeController CreateSizesController();

        DoughController CreateDoughsController();

        AdditionalIngredientController CreateAdditionalIngredientsController();
    }
}
