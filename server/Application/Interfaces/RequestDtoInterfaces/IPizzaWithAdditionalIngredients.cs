namespace Application.Interfaces
{
    using System.Collections.Generic;

    public interface IPizzaWithAdditionalIngredients
    {
        public IEnumerable<int> AdditionalIngredients { get; set; }
    }
}
