namespace Application.Interfaces
{
    using System.Collections.Generic;

    public interface IPizzaWithAdditionalIngredients
    {
        public IEnumerable<string> AdditionalIngredients { get; set; }
    }
}
