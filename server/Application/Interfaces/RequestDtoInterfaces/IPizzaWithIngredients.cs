namespace Application.Interfaces
{
    using System.Collections.Generic;

    public interface IPizzaWithIngredients
    {
        public IEnumerable<string> Ingredients { get; set; }
    }
}
