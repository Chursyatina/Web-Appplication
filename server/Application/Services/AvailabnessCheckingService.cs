namespace Application.Services
{
    using Domain.Models;

    public static class AvailabnessCheckingService
    {
        public static bool GetAvialebnessForPizza(Pizza pizza)
        {
            foreach (Ingredient ingredient in pizza.Ingredients)
            {
                if (!ingredient.IsAvailable)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
