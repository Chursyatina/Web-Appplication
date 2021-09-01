namespace Application.Interfaces
{
    using System.Collections.Generic;

    public interface IPizzaWithIngredients
    {
        public IEnumerable<int> Ingredients { get; set; }
    }
}
