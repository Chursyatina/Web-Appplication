namespace Application.Interfaces
{
    public interface IPizzaVariationWithBothIngredientsTypes : IPizzaVariationRequestDto, IPizzaWithIngredients, IPizzaWithAdditionalIngredients
    {
    }
}
