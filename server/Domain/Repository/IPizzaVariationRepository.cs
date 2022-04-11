namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IPizzaVariationRepository : IRepository<PizzaVariation>
    {
        public PizzaVariation Patch(int id, PizzaVariation item, int? pizzaId, int? doughId, int? sizeId, List<int> ingredientsIds, List<int> additionalIngredientsIds);

        public PizzaVariation Insert(PizzaVariation item, int pizzaId, int sizeId, int doughId);

        public PizzaVariation Insert(PizzaVariation item, int pizzaId, int sizeId, int doughId, List<int> ingredientsIds, List<int> additionalIngredientsIds);

        public IEnumerable<int> GetIdentificators();

        public PizzaVariation Update(int id, PizzaVariation item, int pizzaId, int doughId, int sizeId, List<int> ingredientsIds, List<int> additionalIngredientsIds);
    }
}
