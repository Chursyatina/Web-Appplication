namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using Application.Interfaces;

    public class PizzaVariationPatchRequestDto : IPizzaVariationWithBothIngredientsTypes
    {
        public int? PizzaId { get; set; }

        public int? SizeId { get; set; }

        public int? DoughId { get; set; }

        public IEnumerable<int> AdditionalIngredients { get; set; }

        public IEnumerable<int> Ingredients { get; set; }
    }
}
