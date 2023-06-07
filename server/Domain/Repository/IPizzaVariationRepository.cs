namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IPizzaVariationRepository : IRepository<PizzaVariation>
    {
        public PizzaVariation Patch(string id, PizzaVariation item, string pizzaId, string doughId, string sizeId, List<string> ingredientsIds, List<string> additionalIngredientsIds);

        public PizzaVariation Insert(PizzaVariation item, string pizzaId, string sizeId, string doughId);

        public PizzaVariation Insert(PizzaVariation item, string pizzaId, string sizeId, string doughId, List<string> ingredientsIds, List<string> additionalIngredientsIds);

        public IEnumerable<string> GetIdentificators();

        public PizzaVariation Update(string id, PizzaVariation item, string pizzaId, string doughId, string sizeId, List<string> ingredientsIds, List<string> additionalIngredientsIds);
    }
}
